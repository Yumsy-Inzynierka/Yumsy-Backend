using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Users.FollowUser;

public class FollowUserHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public FollowUserHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FollowUserResponse> Handle(FollowUserRequest followUserRequest, CancellationToken cancellationToken)
    {
        var follower = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == followUserRequest.FollowerId, cancellationToken);

        if (follower == null)
            throw new KeyNotFoundException($"User with ID: {followUserRequest.FollowerId} not found.");
    
        var following = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == followUserRequest.FollowingId, cancellationToken);

        if (following == null)
            throw new KeyNotFoundException($"User with ID: {followUserRequest.FollowingId} not found.");

        var alreadyFollowed = await _dbContext.UserFollowers
            .AnyAsync(l => l.FollowerId == followUserRequest.FollowerId && l.FollowingId == followUserRequest.FollowingId, cancellationToken);

        if (alreadyFollowed)
            throw new InvalidOperationException($"User with Id: {followUserRequest.FollowerId} already follows user with Id: {followUserRequest.FollowingId}.");
    
        var follow = new UserFollower
        {
            FollowingId = followUserRequest.FollowingId,
            FollowerId = followUserRequest.FollowerId
        };

        follower.FollowersCount += 1;
        following.FollowingCount += 1;

        await _dbContext.UserFollowers.AddAsync(follow, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new FollowUserResponse
        {
            FollowingId = follow.FollowingId,
        };
    }
}