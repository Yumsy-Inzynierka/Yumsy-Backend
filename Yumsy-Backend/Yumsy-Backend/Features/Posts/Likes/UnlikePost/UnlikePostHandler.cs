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
        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);
        
        if (like == null)
            throw new InvalidOperationException($"User with ID: {request.UserId} does not like post with ID: {request.PostId}.");

        _dbContext.Likes.Remove(like);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}