using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.LikePost;

public class LikePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public LikePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LikePostResponse> Handle(LikePostRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var alreadyLiked = await _dbContext.Likes
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == userId);

        if (!alreadyLiked)
        {
            var like = new Like()
            {
                UserId = userId,
                PostId = request.PostId
            };

            _dbContext.Likes.Add(like);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            post.LikesCount = await _dbContext.Likes.CountAsync(l => l.PostId == request.PostId, cancellationToken);

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