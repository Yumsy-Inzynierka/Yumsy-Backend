using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Users.UnfollowUser;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class UnfollowUserHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_FollowerDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new UnfollowUserHandler(context);
        var followerId = Guid.NewGuid();
        var request = new UnfollowUserRequest
        {
            FollowerId = followerId,
            Body = new UnfollowUserRequestBody { FollowingId = Guid.NewGuid() }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {followerId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_FollowingUserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var follower = TestDataBuilder.CreateUser();
        context.Users.Add(follower);
        await context.SaveChangesAsync();

        var followingId = Guid.NewGuid();
        var handler = new UnfollowUserHandler(context);
        var request = new UnfollowUserRequest
        {
            FollowerId = follower.Id,
            Body = new UnfollowUserRequestBody { FollowingId = followingId }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {followingId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_NotFollowing()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var follower = TestDataBuilder.CreateUser();
        var following = TestDataBuilder.CreateUser();
        context.Users.AddRange(follower, following);
        await context.SaveChangesAsync();

        var handler = new UnfollowUserHandler(context);
        var request = new UnfollowUserRequest
        {
            FollowerId = follower.Id,
            Body = new UnfollowUserRequestBody { FollowingId = following.Id }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID: {follower.Id} does not follow user with ID: {following.Id}.");
    }

    [Fact]
    public async Task Handle_Should_RemoveFollow_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var follower = TestDataBuilder.CreateUser();
        var following = TestDataBuilder.CreateUser();
        var userFollower = TestDataBuilder.CreateUserFollower(followerId: follower.Id, followingId: following.Id);

        context.Users.AddRange(follower, following);
        context.UserFollowers.Add(userFollower);
        await context.SaveChangesAsync();

        var handler = new UnfollowUserHandler(context);
        var request = new UnfollowUserRequest
        {
            FollowerId = follower.Id,
            Body = new UnfollowUserRequestBody { FollowingId = following.Id }
        };

        await handler.Handle(request, CancellationToken.None);

        var followExists = await context.UserFollowers
            .AnyAsync(f => f.FollowerId == follower.Id && f.FollowingId == following.Id);

        followExists.Should().BeFalse();
    }
}
