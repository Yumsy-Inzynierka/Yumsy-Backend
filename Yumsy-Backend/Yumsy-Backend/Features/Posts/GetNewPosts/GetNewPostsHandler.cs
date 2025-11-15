using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetNewPosts;

public class GetNewPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetNewPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetNewPostsResponse> Handle(GetNewPostsRequest request, CancellationToken cancellationToken)
    {
        var hoursSince = -24;
        var since = DateTime.UtcNow.AddHours(hoursSince);
    
        List<GetNewPostResponse> posts;

        do
        {
            posts = await GetPosts(since, request.UserId, cancellationToken);
            if (posts.Count >= YumsyConstants.NEW_POSTS_AMOUNT || hoursSince < -24 * 90)
                break;

            hoursSince *= 2;
            since = DateTime.UtcNow.AddHours(hoursSince);

        } while (true);

        var newPosts = posts.Take(YumsyConstants.NEW_POSTS_AMOUNT).ToList();

        return new GetNewPostsResponse
        {
            Posts = newPosts,
        };
    }
    
    private async Task<List<GetNewPostResponse>> GetPosts(DateTime since, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts
            .Where(p => p.PostedDate >= since)
            .OrderByDescending(p => p.PostedDate)
            .Select(p => new GetNewPostResponse
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                Image = p.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
                TimePosted = p.PostedDate,
                IsLiked = p.Likes.Any(l => l.UserId == userId),
            })
            .ToListAsync(cancellationToken);
    }
}