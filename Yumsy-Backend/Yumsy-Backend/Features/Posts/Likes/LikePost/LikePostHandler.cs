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
        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var alreadyLiked = await _dbContext.Likes
            .AsNoTracking()
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);

        if (alreadyLiked)
            throw new KeyNotFoundException($"User with ID: {request.UserId} have already liked post with ID: {request.PostId}");

        var like = new Like
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        await _dbContext.Likes.AddAsync(like, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}