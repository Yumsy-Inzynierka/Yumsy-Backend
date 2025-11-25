using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikePost;

public class UnlikePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public UnlikePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnlikePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);
        
        if (like == null)
            throw new InvalidOperationException($"User with ID: {request.UserId} does not like post with ID: {request.PostId}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var tagIds = await _dbContext.PostTags
                .Where(t => t.PostId == request.PostId)
                .Select(i => i.TagId)
                .ToListAsync(cancellationToken);

            if (tagIds.Any())
            {
                await _dbContext.Recommendations
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId) && r.Score >= 1)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(r => r.Score, r => r.Score - 1), cancellationToken);

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
            
            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.LikesCount = await _dbContext.Likes
                .CountAsync(l => l.PostId == request.PostId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}