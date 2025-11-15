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

    public async Task<FollowUserResponse> Handle(FollowUserRequest request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Where(u => u.Id == request.FollowerId || u.Id == request.Body.FollowingId)
            .ToListAsync(cancellationToken);

        var follower = users.FirstOrDefault(u => u.Id == request.FollowerId);
        var following = users.FirstOrDefault(u => u.Id == request.Body.FollowingId);

        if (follower is null)
            throw new KeyNotFoundException($"User with ID: {request.FollowerId} not found.");

        if (following is null)
            throw new KeyNotFoundException($"User with ID: {request.Body.FollowingId} not found.");

        if (follower.Id == following.Id)
            throw new InvalidOperationException($"User with ID: {request.FollowerId} cannot follow themselves.");

        var alreadyFollowed = await _dbContext.UserFollowers
            .AnyAsync(l => l.FollowerId == request.FollowerId && l.FollowingId == request.Body.FollowingId, cancellationToken);

        if (alreadyFollowed)
            throw new InvalidOperationException($"User with ID: {request.FollowerId} already follows user with ID: {request.Body.FollowingId}.");

        var follow = new UserFollower
        {
            FollowingId = request.Body.FollowingId,
            FollowerId = request.FollowerId
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        await _dbContext.UserFollowers.AddAsync(follow, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        follower.FollowingCount = await _dbContext.UserFollowers.CountAsync(l => l.FollowerId == request.FollowerId, cancellationToken);
        following.FollowersCount = await _dbContext.UserFollowers.CountAsync(l => l.FollowingId == request.Body.FollowingId, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return new FollowUserResponse
        {
            FollowingId = follow.FollowingId
        };
    }
}
