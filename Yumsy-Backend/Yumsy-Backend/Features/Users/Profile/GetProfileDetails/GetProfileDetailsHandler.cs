using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

public class GetProfileDetailsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetProfileDetailsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetProfileDetailsResponse> Handle(GetProfileDetailsRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var profile = await _dbContext.Users
            .AsNoTracking()
            .Include(p => p.Posts)
            .ThenInclude(p => p.PostImages)
            .FirstOrDefaultAsync(p => p.Id == request.ProfileOwnerId, cancellationToken);

        if (profile == null)
            throw new KeyNotFoundException($"User with ID: {request.ProfileOwnerId} not found.");

        bool? isFollowed = null;

        if (request.UserId != request.ProfileOwnerId)
        {
            isFollowed = await _dbContext.UserFollowers
                .AsNoTracking()
                .AnyAsync(l => l.FollowerId == request.UserId && l.FollowingId == request.ProfileOwnerId, cancellationToken);
        }

        return new GetProfileDetailsResponse
        {
            Id = profile.Id,
            IsFollowed = isFollowed,
            Username = profile.Username,
            ProfileName = profile.ProfileName ?? profile.Username,
            RecipesCount = profile.RecipesCount,
            FollowersCount = profile.FollowersCount,
            RecreationsCount = 0,
            Bio = profile.Bio,
            ProfilePicture = profile.ProfilePicture,
            ProfilePosts = profile.Posts
                .Select(p => new GetProfileDetailsPostsResponse
                {
                    Id = p.Id,
                    Image = p.PostImages.FirstOrDefault()?.ImageUrl
                })
                .ToList()
        };
    }
}
