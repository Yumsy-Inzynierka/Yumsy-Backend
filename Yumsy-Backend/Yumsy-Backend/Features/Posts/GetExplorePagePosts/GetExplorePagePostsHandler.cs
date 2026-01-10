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
        var recommendedPosts = await _dbContext.Database
            .SqlQueryRaw<GetExplorePagePostResponseDTO>(@"
                SELECT 
                    post_id AS ""Id""
                FROM recommend_posts({0}, {1}, {2}, {3})",
                request.UserId, 7, 6 ,3)
            .ToListAsync(cancellationToken);

        if (!recommendedPosts.Any())
        {
            return new GetExplorePagePostsResponse
            {
                Posts = new List<GetExplorePagePostResponse>()
            };
        }

        var recommendedPostIds = recommendedPosts.Select(r => r.Id).ToList();
        
        var posts = await _dbContext.Posts
            .Where(p => recommendedPostIds.Contains(p.Id))
            .Select(p => new GetExplorePagePostResponse
            {
                Id = p.Id,
                Image = p.PostImages
                    .Select(i => i.ImageUrl)
                    .FirstOrDefault(),
                CookingTime = p.CookingTime,
                Tags = p.PostTags.Select(pt => new GetExplorePagePostTagResponse
                {
                    Id = pt.Tag.Id,
                    Name = pt.Tag.Name
                })
            })
            .ToListAsync(cancellationToken);
        
        var rnd = new Random();
        posts = posts
            .OrderBy(p => rnd.Next())
            .ToList();
        
        var seenPosts = posts.Select(r => new SeenPost
        {
            UserId = request.UserId,
            PostId = r.Id,
        }).ToList();

        await _dbContext.SeenPosts.AddRangeAsync(seenPosts, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new GetExplorePagePostsResponse
        {
            Posts = posts
        };
    }
}