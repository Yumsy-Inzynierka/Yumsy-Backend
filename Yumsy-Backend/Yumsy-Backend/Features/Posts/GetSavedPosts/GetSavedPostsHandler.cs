using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public class GetSavedPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetSavedPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetSavedPostsResponse> Handle(GetSavedPostsRequest request, Guid userId, 
        CancellationToken cancellationToken)
    {
        var page = request.CurrentPage;
        
        var query = _dbContext.Saved
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.SavedAt)
            .Join(
                _dbContext.Posts,
                saved => saved.PostId,
                post => post.Id,
                (saved, post) => post
                );
        
        int totalCount = await query.CountAsync(cancellationToken);
        
        var posts = await query
            .Skip((int)(page - 1) * YumsyConstants.SAVED_POSTS_AMOUNT)
            .Take(YumsyConstants.SAVED_POSTS_AMOUNT)
            .Select(post => new GetSavedPostResponse
            {
                Id = post.Id,
                Image = _dbContext.PostImages
                    .Where(pi => pi.PostId == post.Id)
                    .OrderBy(pi => pi.Id)
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault()
            })
            .ToListAsync(cancellationToken);

        return new GetSavedPostsResponse
        {
            SavedPosts = posts,
            CurrentPage = page,
            HasMore = page * YumsyConstants.SAVED_POSTS_AMOUNT < totalCount
        };
    }
}