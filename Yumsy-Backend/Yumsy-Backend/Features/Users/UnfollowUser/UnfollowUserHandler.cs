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
            .FirstOrDefaultAsync(u => u.Id == unfollowUserRequest.Body.FollowingId, cancellationToken);

        if (following == null)
            throw new KeyNotFoundException($"User with ID: {unfollowUserRequest.Body.FollowingId} not found.");

        var follow = await _dbContext.UserFollowers
            .FirstOrDefaultAsync(l => 
                l.FollowerId == unfollowUserRequest.FollowerId && 
                l.FollowingId == unfollowUserRequest.Body.FollowingId, 
                cancellationToken);

        if (follow == null)
            throw new InvalidOperationException($"User with ID: {unfollowUserRequest.FollowerId} does not follow user with ID: {unfollowUserRequest.Body.FollowingId}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _dbContext.UserFollowers.Remove(follow);
            await _dbContext.SaveChangesAsync(cancellationToken);
        
            follower.FollowingCount = await _dbContext.UserFollowers
                .CountAsync(l => l.FollowerId == unfollowUserRequest.FollowerId, cancellationToken);
            
            following.FollowersCount = await _dbContext.UserFollowers
                .CountAsync(l => l.FollowingId == unfollowUserRequest.Body.FollowingId, cancellationToken);
        
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
