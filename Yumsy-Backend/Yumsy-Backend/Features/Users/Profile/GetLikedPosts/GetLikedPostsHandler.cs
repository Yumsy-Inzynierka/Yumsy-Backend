using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

public class GetLikedPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetLikedPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetLikedPostsResponse> Handle(Guid userId, 
        CancellationToken cancellationToken)
    {
        var posts = await _dbContext.Likes
            .Where(l => l.UserId == userId)
            .Select(l => new GetLikedPostResponse
            {
                Id = l.PostId,
                Image = l.Post.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
            })
            .ToListAsync(cancellationToken);
            
        
        return new GetLikedPostsResponse
        {
            Posts = posts
        };
    }
}