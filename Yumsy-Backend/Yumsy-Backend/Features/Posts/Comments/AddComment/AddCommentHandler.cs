using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Comments.AddComment;

public class AddCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public AddCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddCommentResponse> Handle(AddCommentRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");
    
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(u => u.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");
        
        if (request.Body.ParentCommentId.HasValue)
        {
            var parentExists = await _dbContext.Comments
                .AnyAsync(c => c.Id == request.Body.ParentCommentId.Value, cancellationToken);
    
            if (!parentExists)
                throw new KeyNotFoundException($"Parent comment with ID: {request.Body.ParentCommentId} not found.");
        }
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = request.Body.Content,
            PostId = request.PostId,
            ParentCommentId = request.Body.ParentCommentId,
            UserId = request.UserId,
            CommentedDate = DateTime.UtcNow,
        };
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.CommentsCount = await _dbContext.Comments
                .CountAsync(c => c.PostId == request.PostId, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var tagIds = await _dbContext.PostTags
                .Where(t => t.PostId == request.PostId)
                .Select(i => i.TagId)
                .ToListAsync(cancellationToken);

            if (tagIds.Any())
            {
                await _dbContext.Recommendations
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId))
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Score, r => r.Score + 2), cancellationToken);

                var existingTagIds = await _dbContext.Recommendations
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId))
                    .Select(r => r.TagId)
                    .ToListAsync(cancellationToken);

                var newTagIds = tagIds.Except(existingTagIds).ToList();

                if (newTagIds.Any())
                {
                    var newRecs = newTagIds.Select(tagId => new Recommendation
                    {
                        TagId = tagId,
                        UserId = request.UserId,
                        Score = 2
                    });

                    _dbContext.Recommendations.AddRange(newRecs);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        
        return new AddCommentResponse()
        {
            Id = comment.Id,
            Content = comment.Content,
            CommentedDate = comment.CommentedDate,
            PostId = comment.PostId,
            UserId = comment.UserId,
            ParentCommentId = comment.ParentCommentId
        };
    }
}