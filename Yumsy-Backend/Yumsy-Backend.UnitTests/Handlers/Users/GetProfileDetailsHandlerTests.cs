using FluentAssertions;
using Yumsy_Backend.Features.Users.Profile.GetProfileDetails;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class GetProfileDetailsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetProfileDetailsHandler(context);
        var userId = Guid.NewGuid();
        var request = new GetProfileDetailsRequest
        {
            UserId = userId,
            ProfileOwnerId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_ProfileOwnerDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var profileOwnerId = Guid.NewGuid();
        var handler = new GetProfileDetailsHandler(context);
        var request = new GetProfileDetailsRequest
        {
            UserId = user.Id,
            ProfileOwnerId = profileOwnerId
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {profileOwnerId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnProfileDetails_When_ViewingOwnProfile()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new GetProfileDetailsHandler(context);
        var request = new GetProfileDetailsRequest
        {
            UserId = user.Id,
            ProfileOwnerId = user.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.Username.Should().Be(user.Username);
        result.IsFollowed.Should().BeNull();
    }

    [Fact]
    public async Task Handle_Should_ReturnIsFollowedFalse_When_NotFollowing()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var profileOwner = TestDataBuilder.CreateUser();
        context.Users.AddRange(user, profileOwner);
        await context.SaveChangesAsync();

        var handler = new GetProfileDetailsHandler(context);
        var request = new GetProfileDetailsRequest
        {
            UserId = user.Id,
            ProfileOwnerId = profileOwner.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFollowed.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnIsFollowedTrue_When_Following()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var profileOwner = TestDataBuilder.CreateUser();
        var follow = TestDataBuilder.CreateUserFollower(followerId: user.Id, followingId: profileOwner.Id);

        context.Users.AddRange(user, profileOwner);
        context.UserFollowers.Add(follow);
        await context.SaveChangesAsync();

        var handler = new GetProfileDetailsHandler(context);
        var request = new GetProfileDetailsRequest
        {
            UserId = user.Id,
            ProfileOwnerId = profileOwner.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsFollowed.Should().BeTrue();
    }
}
