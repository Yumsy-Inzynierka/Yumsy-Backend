using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.SearchPosts;

public class SearchPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public SearchPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SearchPostsResponse> Handle(SearchPostsRequest searchPostsRequest, 
        CancellationToken cancellationToken)
    {
        var page = searchPostsRequest.Page;
        var offset = (page - 1) * YumsyConstants.SEARCH_POSTS_AMOUNT;
        
        var searchResult = await _dbContext.Database.SqlQueryRaw<SearchPostResponse>(@"
                SELECT 
                    id, 
                    post_title, 
                    user_id, 
                    username, 
                    image_url, 
                    cooking_time, 
                    relevance_score
                FROM search_posts({0}:: TEXT, {1}::INT)", 
                searchPostsRequest.Query, offset
            )
            .ToListAsync(cancellationToken);
        
        var nextPageOffset = offset + YumsyConstants.SEARCH_POSTS_AMOUNT;
        var hasMoreCheck = await _dbContext.Database
            .SqlQueryRaw<SearchPostResponse>(@"
                SELECT 
                    id, 
                    post_title, 
                    user_id, 
                    username, 
                    image_url, 
                    cooking_time, 
                    relevance_score
                FROM search_posts(
                    {0}::TEXT,
                    {1}::INT
                )
                LIMIT 1",
                searchPostsRequest.Query, nextPageOffset
            )
            .ToListAsync(cancellationToken);
        
        var hasMore = hasMoreCheck.Any();

        return new SearchPostsResponse
        {
            Posts = searchResult,
            Page = page,
            HasMore = hasMore,
        };

    }
}