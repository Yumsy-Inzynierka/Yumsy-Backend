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

    public async Task<UnlikePostResponse> Handle(UnlikePostRequest unlikePostRequest, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == unlikePostRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {unlikePostRequest.PostId} not found");

        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == unlikePostRequest.PostId && l.UserId == unlikePostRequest.UserId, cancellationToken);

        if (like != null)
        {
            _dbContext.Likes.Remove(like);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.LikesCount = await _dbContext.Likes.CountAsync(l => l.PostId == unlikePostRequest.PostId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        
        return new UnlikePostResponse()
        {
            Id = post.Id,
            Liked = false,
            LikesCount = post.LikesCount
        };
    }
}