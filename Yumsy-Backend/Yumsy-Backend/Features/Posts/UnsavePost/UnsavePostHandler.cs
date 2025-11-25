using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

public class UnsavePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public UnsavePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnsavePostRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");

        var savedPost = await _dbContext.Saved
            .FirstOrDefaultAsync(s => s.PostId == request.PostId && s.UserId == request.UserId, cancellationToken);

        if (savedPost == null)
            throw new InvalidOperationException("Post is already unsaved by this user.");

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
                    .Where(r => r.UserId == request.UserId && tagIds.Contains(r.TagId) && r.Score >= 3)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(r => r.Score, r => r.Score - 3), cancellationToken);

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
                        Score = 3
                    });

                    _dbContext.Recommendations.AddRange(newRecs);
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }
            
            _dbContext.Saved.Remove(savedPost);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.SavedCount = await _dbContext.Saved
                .CountAsync(s => s.PostId == request.PostId, cancellationToken);

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