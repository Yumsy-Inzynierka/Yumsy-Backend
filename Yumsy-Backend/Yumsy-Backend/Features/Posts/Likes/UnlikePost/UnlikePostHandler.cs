using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.UnlikePost;

public class UnlikePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public UnlikePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UnlikePostResponse> Handle(UnlikePostRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Likes)
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var like = await _dbContext.Likes
            .FirstOrDefaultAsync(l => l.PostId == request.PostId && l.UserId == userId);

        if (like != null)
        {
            _dbContext.Likes.Remove(like);

            post.LikesCount = _dbContext.Likes.Count(l => l.PostId == request.PostId);;

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