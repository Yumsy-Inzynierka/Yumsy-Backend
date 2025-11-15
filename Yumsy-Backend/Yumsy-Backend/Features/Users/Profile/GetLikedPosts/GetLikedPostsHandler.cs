using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

public class GetLikedPostsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetLikedPostsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetLikedPostsResponse> Handle(GetLikedPostsRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var page = request.CurrentPage;
        
        ///do spradzenia i możliwe że do zmiany
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == userId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {userId} not found.");

        var query = _dbContext.Likes
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.CreatedAt)
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
            .Select(l => new GetLikedPostResponse
            {
                Id = l.Id,
                Image = l.PostImages
                    .Select(pi => pi.ImageUrl)
                    .FirstOrDefault(),
            })
            .ToListAsync(cancellationToken);
        
        return new GetLikedPostsResponse
        {
            Posts = posts,
            CurrentPage = page,
            HasMore = page * YumsyConstants.LIKED_POSTS_AMOUNT < totalCount
        };
    }
}