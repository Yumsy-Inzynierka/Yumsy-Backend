using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.UnfollowUser;

public class UnfollowUserHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public UnfollowUserHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnfollowUserRequest unfollowUserRequest, CancellationToken cancellationToken)
    {
        var follower = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == unfollowUserRequest.FollowerId, cancellationToken);

        if (follower == null)
            throw new KeyNotFoundException($"User with ID: {unfollowUserRequest.FollowerId} not found.");
    
        var following = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == unfollowUserRequest.FollowingId, cancellationToken);

        if (following == null)
            throw new KeyNotFoundException($"User with ID: {unfollowUserRequest.FollowingId} not found.");

        var follow = await _dbContext.UserFollowers
            .FirstOrDefaultAsync(l => l.FollowerId == unfollowUserRequest.FollowerId && l.FollowingId == unfollowUserRequest.FollowingId, cancellationToken);

        if (follow == null)
            throw new InvalidOperationException($"User with Id: {unfollowUserRequest.FollowerId} already does not follow user with Id: {unfollowUserRequest.FollowingId}.");

        _dbContext.UserFollowers.Remove(follow);
        follower.FollowingCount = _dbContext.UserFollowers.Count(l => l.FollowerId == unfollowUserRequest.FollowerId);
        following.FollowersCount = _dbContext.UserFollowers.Count(l => l.FollowingId == unfollowUserRequest.FollowingId);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}