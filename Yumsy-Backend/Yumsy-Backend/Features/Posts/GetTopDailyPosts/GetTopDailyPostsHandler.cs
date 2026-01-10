using System.Diagnostics;
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
        var sw = Stopwatch.StartNew();
    
        var posts = await _dbContext.TopDailyPosts
            .AsNoTracking()
            .OrderByDescending(tdp => tdp.Date)
            .ThenBy(tdp => tdp.Rank)
            .Select(tdp => new GetTopDailyPostResponse
            {
                Id = tdp.PostId,
                PostTitle = tdp.Title, 
                UserId = tdp.UserId,
                Username = tdp.Username,
                Image = tdp.ImageUrl,
                TimePosted = tdp.PostedDate,
                IsLiked = _dbContext.Likes.Any(l => l.PostId == tdp.PostId && l.UserId == request.UserId),
            })
            .ToListAsync(cancellationToken);
    
        sw.Stop();
        Console.WriteLine($"GetTopDailyPostsHandler.Handle {sw.ElapsedMilliseconds}ms");
        
        return new GetTopDailyPostsResponse
        {
            Posts = posts
        };
    }
}