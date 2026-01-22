using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Users.FollowUser;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class FollowUserHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_UserTriesToFollowThemselves()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var userId = Guid.NewGuid();
        var handler = new FollowUserHandler(context);
        var request = new FollowUserRequest
        {
            FollowerId = userId,
            Body = new FollowUserRequestBody { FollowingId = userId }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("User cannot follow themselves");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_FollowerDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var followingUser = TestDataBuilder.CreateUser();
        context.Users.Add(followingUser);
        await context.SaveChangesAsync();

        var followerId = Guid.NewGuid();
        var handler = new FollowUserHandler(context);
        var request = new FollowUserRequest
        {
            FollowerId = followerId,
            Body = new FollowUserRequestBody { FollowingId = followingUser.Id }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {followerId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_FollowingUserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var followerUser = TestDataBuilder.CreateUser();
        context.Users.Add(followerUser);
        await context.SaveChangesAsync();

        var followingId = Guid.NewGuid();
        var handler = new FollowUserHandler(context);
        var request = new FollowUserRequest
        {
            FollowerId = followerUser.Id,
            Body = new FollowUserRequestBody { FollowingId = followingId }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {followingId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_AlreadyFollowing()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var follower = TestDataBuilder.CreateUser();
        var following = TestDataBuilder.CreateUser();
        var existingFollow = TestDataBuilder.CreateUserFollower(followerId: follower.Id, followingId: following.Id);

        context.Users.AddRange(follower, following);
        context.UserFollowers.Add(existingFollow);
        await context.SaveChangesAsync();

        var handler = new FollowUserHandler(context);
        var request = new FollowUserRequest
        {
            FollowerId = follower.Id,
            Body = new FollowUserRequestBody { FollowingId = following.Id }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("User already follows this user");
    }

    [Fact]
    public async Task Handle_Should_CreateFollow_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var follower = TestDataBuilder.CreateUser();
        var following = TestDataBuilder.CreateUser();

        context.Users.AddRange(follower, following);
        await context.SaveChangesAsync();

        var handler = new FollowUserHandler(context);
        var request = new FollowUserRequest
        {
            FollowerId = follower.Id,
            Body = new FollowUserRequestBody { FollowingId = following.Id }
        };

        await handler.Handle(request, CancellationToken.None);

        var follow = await context.UserFollowers
            .FirstOrDefaultAsync(f => f.FollowerId == follower.Id && f.FollowingId == following.Id);

        follow.Should().NotBeNull();
        follow!.FollowerId.Should().Be(follower.Id);
        follow.FollowingId.Should().Be(following.Id);
    }
}
