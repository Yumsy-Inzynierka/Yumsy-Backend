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

    public async Task<GetExplorePagePostsResponse> Handle(GetExplorePagePostsRequest request, CancellationToken cancellationToken)
    {
        // na razie nie zmieniam bo i tak jest logika do zmiany
        var query = _dbContext.Posts
            .Include(p => p.PostImages)
            .AsNoTracking();

        var totalCount = await query.CountAsync(cancellationToken);
        
        
        return new GetExplorePagePostsResponse
        {
            CurrentPage = request.CurrentPage,
        };
    }
}