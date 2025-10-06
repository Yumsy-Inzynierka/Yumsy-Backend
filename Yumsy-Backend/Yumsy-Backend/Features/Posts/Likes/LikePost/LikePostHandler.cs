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

    public async Task<LikePostResponse> Handle(LikePostRequest likePostRequest, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == likePostRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {likePostRequest.PostId} not found");

        var alreadyLiked = await _dbContext.Likes
            .AnyAsync(l => l.PostId == likePostRequest.PostId && l.UserId == likePostRequest.UserId);

        if (!alreadyLiked)
        {
            var like = new Like()
            {
                UserId = likePostRequest.UserId,
                PostId = likePostRequest.PostId
            };

            _dbContext.Likes.Add(like);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            post.LikesCount = await _dbContext.Likes.CountAsync(l => l.PostId == likePostRequest.PostId, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        
        return new LikePostResponse
        {
            Id = post.Id,
            Liked = true,
            LikesCount = post.LikesCount
        };
    }
}