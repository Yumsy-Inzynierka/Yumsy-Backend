using FluentAssertions;
using Yumsy_Backend.Features.Users.SearchUsers;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class SearchUsersHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoUsersMatch()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new SearchUsersHandler(context);
        var request = new SearchUsersRequest
        {
            Query = "nonexistent",
            Page = 1
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Users.Should().BeEmpty();
        result.HasMore.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnUsers_When_UsernameMatches()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        user.Username = "testuser123";
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new SearchUsersHandler(context);
        var request = new SearchUsersRequest
        {
            Query = "testuser",
            Page = 1
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Users.Should().HaveCount(1);
        result.Users.First().Username.Should().Be("testuser123");
    }

    [Fact]
    public async Task Handle_Should_ReturnUsers_When_ProfileNameMatches()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        user.ProfileName = "Chef John";
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new SearchUsersHandler(context);
        var request = new SearchUsersRequest
        {
            Query = "Chef",
            Page = 1
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Users.Should().HaveCount(1);
        result.Users.First().ProfileName.Should().Be("Chef John");
    }

    [Fact]
    public async Task Handle_Should_BeCaseInsensitive()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        user.Username = "TestUser";
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new SearchUsersHandler(context);
        var request = new SearchUsersRequest
        {
            Query = "TESTUSER",
            Page = 1
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Users.Should().HaveCount(1);
    }
}
