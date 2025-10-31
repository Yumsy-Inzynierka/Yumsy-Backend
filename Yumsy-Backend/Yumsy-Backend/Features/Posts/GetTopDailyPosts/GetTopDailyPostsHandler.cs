using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetTopDailyPosts;

public class GetTopDailyPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetTopDailyPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTopDailyPostsResponse> Handle(CancellationToken cancellationToken)
    {
        var hoursSince = -24;
        var since = DateTime.UtcNow.AddHours(hoursSince);
        const int pool = 15;
        
        var posts = GetPosts(since, cancellationToken);

        while (posts.Result.Count < pool)
        {
            hoursSince *= 2;
            since = DateTime.UtcNow.AddHours(hoursSince);
            posts = GetPosts(since, cancellationToken);
            
            if (hoursSince < -24 * 90)
                break;
        }
        
        var topPosts = posts.Result.Take(YumsyConstants.TOP_DAILY_POSTS_AMOUNT).ToList();
        
        return new GetTopDailyPostsResponse
        {
            Posts = topPosts,
        };
    }

    private async Task<List<GetTopDailyPostResponse>> GetPosts(DateTime since, CancellationToken cancellationToken)
    {
        return await _dbContext.Posts
            .Where(p => p.PostedDate >= since)
            .OrderByDescending(p => p.LikesCount + p.CommentsCount + p.SavedCount)
            .Select(p => new GetTopDailyPostResponse
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                Image = p.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
                TimePosted = p.PostedDate,
                Count = p.LikesCount + p.CommentsCount + p.SavedCount
            })
            .ToListAsync(cancellationToken);
    }
}