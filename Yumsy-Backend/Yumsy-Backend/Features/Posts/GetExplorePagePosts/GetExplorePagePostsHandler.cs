using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

public class GetExplorePagePostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetExplorePagePostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetExplorePagePostsResponse> Handle(GetExplorePagePostsRequest request, CancellationToken cancellationToken)
    {
        var recommendationResult = await _dbContext.RecommendPosts
            .FromSqlRaw(
                "SELECT * FROM recommend_posts({0}, {1}, {2}, {3})",
                request.UserId, 6, 6, 3)
            .Select(rr => new GetExplorePagePostResponse
            {
                Id = rr.post_id,
                Image = rr.image,
            })
            .ToListAsync(cancellationToken);

        var rnd = new Random();
        recommendationResult = recommendationResult
            .OrderBy(x => rnd.Next())
            .ToList();
        
        var seenPosts = recommendationResult.Select(r => new SeenPost
        {
            UserId = request.UserId,
            PostId = r.Id,
        }).ToList();

        await _dbContext.SeenPosts.AddRangeAsync(seenPosts, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new GetExplorePagePostsResponse
        {
            Posts = recommendationResult
        };
    }
}