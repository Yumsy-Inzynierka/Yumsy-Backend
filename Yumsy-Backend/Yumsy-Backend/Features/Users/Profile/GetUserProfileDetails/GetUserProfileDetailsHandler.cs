using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Profile.GetUserProfileDetails;

public class GetUserProfileDetailsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetUserProfileDetailsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetUserProfileDetailsResponse> Handle(GetUserProfileDetailsRequest request)
    {
        var profile = await _dbContext.Users
            .AsNoTracking()
            .Include(p => p.Posts)
            .ThenInclude(p => p.PostImages)
            .FirstOrDefaultAsync(p => p.Id == request.UserId);

        if (profile == null)
            throw new KeyNotFoundException($"User with ID: {request.UserId} does not exist");
        
        
        
        return new GetUserProfileDetailsResponse()
        {
            Id = profile.Id,
            Username = profile.Username,
            ProfileName = profile.ProfileName ?? profile.Username,
            RecipesCount = profile.RecipesCount,
            FollowersCount = profile.FollowersCount,
            RecreationsCount = 0, // to be implemented: recreations logic
            Bio = profile.Bio,
            ProfilePicture = profile.ProfilePicture,
            ProfilePosts = profile.Posts
                .Select(p => new GetUserProfileDetailsPostsResponse()
                {
                    Id = p.Id,
                    Image = p.PostImages.FirstOrDefault()?.ImageUrl
                })
                .ToList()
        };
    }
}