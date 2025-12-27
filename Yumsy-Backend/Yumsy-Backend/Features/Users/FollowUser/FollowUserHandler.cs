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

    public async Task Handle(FollowUserRequest request, CancellationToken cancellationToken)
    {
        if (request.FollowerId == request.Body.FollowingId)
            throw new InvalidOperationException("User cannot follow themselves");

        var userIds = new[] { request.FollowerId, request.Body.FollowingId };
        var existingUsers = await _dbContext.Users
            .Where(u => userIds.Contains(u.Id))
            .Select(u => u.Id)
            .ToListAsync(cancellationToken);

        if (!existingUsers.Contains(request.FollowerId))
            throw new KeyNotFoundException($"User with ID: {request.FollowerId} not found");

        if (!existingUsers.Contains(request.Body.FollowingId))
            throw new KeyNotFoundException($"User with ID: {request.Body.FollowingId} not found");

        var alreadyFollowed = await _dbContext.UserFollowers
            .AnyAsync(
                f => f.FollowerId == request.FollowerId && f.FollowingId == request.Body.FollowingId, 
                cancellationToken);

        if (alreadyFollowed)
            throw new InvalidOperationException("User already follows this user");

        var follow = new UserFollower
        {
            FollowingId = request.Body.FollowingId,
            FollowerId = request.FollowerId
        };

        await _dbContext.UserFollowers.AddAsync(follow, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
