using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetTopDailyPostsEndpoint;

public class GetTopDailyPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetTopDailyPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetTopDailyPostsResponse> Handle(CancellationToken cancellationToken)
    {
        var since = DateTime.UtcNow.AddHours(-24);
        
        var posts = await _dbContext.Posts
            .Where(p => p.PostedDate >= since)
            .OrderByDescending(p => p.LikesCount + p.CommentsCount)
            .Take(YumsyConstants.TOP_DAILY_POSTS_AMOUNT)
            .Select(p => new GetTopDailyPostResponse
            {
                Id = p.Id,
                PostTitle = p.Title,
                UserId = p.UserId,
                Username = p.CreatedBy.Username,
                ImageURL = p.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
                TimePosted = p.PostedDate,
                Count = p.LikesCount + p.CommentsCount
            })
            .ToListAsync(cancellationToken);

        return new GetTopDailyPostsResponse
        {
            Posts = posts
        };
    }
}