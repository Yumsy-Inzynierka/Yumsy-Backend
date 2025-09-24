using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

public class GetExplorePagePostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetExplorePagePostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetExplorePagePostsResponse> Handle(GetExplorePagePostsRequest getExplorePagePostsRequest,
        CancellationToken cancellationToken)
    {
        var query = _dbContext.Posts
            .Include(p => p.PostImages)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        
        var posts = await query
            .Skip((getExplorePagePostsRequest.CurrentPage - 1) * YumsyConstants.FETCHED_POSTS_AMOUNT)
            .OrderBy(x => Guid.NewGuid()) // pseudo losowy algorytm
            .Take(YumsyConstants.FETCHED_POSTS_AMOUNT)
            .Select(p => new GetExplorePagePostResponse
            {
                Id = p.Id,
                ImageUrl = p.PostImages.FirstOrDefault().ImageUrl,
            })
            .ToListAsync(cancellationToken);
        
        return new GetExplorePagePostsResponse
        {
            Posts = posts,
            CurrentPage = getExplorePagePostsRequest.CurrentPage,
            HasMore = getExplorePagePostsRequest.CurrentPage * YumsyConstants.FETCHED_POSTS_AMOUNT < totalCount
        };
    }
}