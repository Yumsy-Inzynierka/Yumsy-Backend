using FluentAssertions;
using Yumsy_Backend.Features.Users.Profile.EditProfileDetails;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class EditProfileDetailsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new EditProfileDetailsHandler(context);
        var userId = Guid.NewGuid();
        var request = new EditProfileDetailsRequest
        {
            UserId = userId,
            Body = new EditProfileDetailsRequestBody
            {
                Username = "newusername",
                ProfileName = "New Name"
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_UsernameIsTaken()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser(username: "user1");
        var otherUser = TestDataBuilder.CreateUser(username: "takenusername");
        context.Users.AddRange(user, otherUser);
        await context.SaveChangesAsync();

        var handler = new EditProfileDetailsHandler(context);
        var request = new EditProfileDetailsRequest
        {
            UserId = user.Id,
            Body = new EditProfileDetailsRequestBody
            {
                Username = "takenusername"
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Username 'takenusername' is already taken.");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_ProfileNameIsTaken()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var otherUser = TestDataBuilder.CreateUser();
        otherUser.ProfileName = "TakenProfileName";
        context.Users.AddRange(user, otherUser);
        await context.SaveChangesAsync();

        var handler = new EditProfileDetailsHandler(context);
        var request = new EditProfileDetailsRequest
        {
            UserId = user.Id,
            Body = new EditProfileDetailsRequestBody
            {
                ProfileName = "TakenProfileName"
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Profile name 'TakenProfileName' is already taken.");
    }

    [Fact]
    public async Task Handle_Should_UpdateProfile_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new EditProfileDetailsHandler(context);
        var request = new EditProfileDetailsRequest
        {
            UserId = user.Id,
            Body = new EditProfileDetailsRequestBody
            {
                Username = "newusername",
                ProfileName = "New Profile Name",
                Bio = "New bio"
            }
        };

        await handler.Handle(request, CancellationToken.None);

        var updatedUser = await context.Users.FindAsync(user.Id);
        updatedUser!.Username.Should().Be("newusername");
        updatedUser.ProfileName.Should().Be("New Profile Name");
        updatedUser.Bio.Should().Be("New bio");
    }
}
