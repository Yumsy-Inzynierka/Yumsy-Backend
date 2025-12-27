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

    public async Task<GetTopDailyPostsResponse> Handle(GetTopDailyPostsRequest request, 
        CancellationToken cancellationToken)
    {
        var latestDate = await _dbContext.TopDailyPosts
            .MaxAsync(tdp => tdp.Date, cancellationToken);
        
        var posts = await _dbContext.TopDailyPosts
            .Where(tdp => tdp.Date == latestDate)
            .OrderBy(tdp => tdp.Rank)
            .Select(tdp => new GetTopDailyPostResponse
            {
                Id = tdp.Post.Id,
                PostTitle = tdp.Post.Title,
                UserId = tdp.Post.UserId,
                Username = tdp.Post.CreatedBy.Username,
                Image = tdp.Post.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
                TimePosted = tdp.Post.PostedDate,
                IsLiked = tdp.Post.Likes.Any(l => l.UserId == request.UserId),
            })
            .ToListAsync(cancellationToken);
        
        return new GetTopDailyPostsResponse
        {
            Posts = posts
        };
    }
}