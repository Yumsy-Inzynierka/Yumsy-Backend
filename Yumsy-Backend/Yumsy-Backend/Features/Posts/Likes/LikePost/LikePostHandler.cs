using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Likes.LikePost;

public class LikePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public LikePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(LikePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var alreadyLiked = await _dbContext.Likes
            .AsNoTracking()
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);

        if (alreadyLiked)
            throw new KeyNotFoundException($"User with ID: {request.UserId} have already liked post with ID: {request.PostId}");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            var like = new Like
            {
                UserId = request.UserId,
                PostId = request.PostId
            };

            await _dbContext.Likes.AddAsync(like, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.LikesCount = await _dbContext.Likes
                .CountAsync(l => l.PostId == request.PostId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            var tagIds = await _dbContext.PostTags
                .Where(t => t.PostId == request.PostId)
                .Select(i => i.TagId)
                .ToListAsync(cancellationToken);

            if (tagIds.Any())
            {
                await _dbContext.Recommendations
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId))
                    .ExecuteUpdateAsync(s => s.SetProperty(r => r.Score, r => r.Score + 1), cancellationToken);

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
                        Score = 1
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