using FluentAssertions;
using Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.ShoppingLists;

public class GetShoppingListsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetShoppingListsHandler(context);
        var userId = Guid.NewGuid();
        var request = new GetShoppingListsRequest
        {
            UserId = userId
        };

        var act = () => handler.Handle(request);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoShoppingLists()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new GetShoppingListsHandler(context);
        var request = new GetShoppingListsRequest
        {
            UserId = user.Id
        };

        var result = await handler.Handle(request);

        result.Should().NotBeNull();
        result.ShoppingLists.Should().BeEmpty();
    }
}
