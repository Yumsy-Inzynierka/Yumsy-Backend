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

    public async Task<LikePostResponse> Handle(LikePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var alreadyLiked = await _dbContext.Likes
            .AsNoTracking()
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            if (!alreadyLiked)
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
            }

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return new LikePostResponse
        {
            Id = post.Id,
            Liked = true,
            LikesCount = post.LikesCount
        };
    }
}