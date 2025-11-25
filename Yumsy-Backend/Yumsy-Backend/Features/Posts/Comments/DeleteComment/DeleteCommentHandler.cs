using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Comments.DeleteComment;

public class DeleteCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public DeleteCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var isUserCommentOwner = await _dbContext.Comments.AnyAsync(c => c.UserId == request.UserId, cancellationToken);

        if (!isUserCommentOwner)
            throw new KeyNotFoundException($"User with ID: {request.UserId} is not owner of this comment.");
        
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");

        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == request.CommentId && c.PostId == request.PostId, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID: {request.CommentId} not found in post with ID: {request.PostId}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.CommentsCount = await _dbContext.Comments.CountAsync(c => c.PostId == request.PostId, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            var tagIds = await _dbContext.PostTags
                .Where(t => t.PostId == request.PostId)
                .Select(i => i.TagId)
                .ToListAsync(cancellationToken);

            if (tagIds.Any())
            {
                await _dbContext.Recommendations
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId) && r.Score >= 2)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(r => r.Score, r => r.Score - 2), cancellationToken);

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
                        Score = 0
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
    }
}