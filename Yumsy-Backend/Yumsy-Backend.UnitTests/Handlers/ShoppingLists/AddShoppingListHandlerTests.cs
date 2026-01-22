using FluentAssertions;
using Yumsy_Backend.Features.ShoppingLists.AddShoppingList;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.ShoppingLists;

public class AddShoppingListHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new AddShoppingListHandler(context);
        var userId = Guid.NewGuid();
        var request = new AddShoppingListRequest
        {
            UserId = userId,
            Body = new AddShoppingListRequestBody
            {
                Title = "Test List",
                CreatedFrom = Guid.NewGuid()
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new AddShoppingListHandler(context);
        var request = new AddShoppingListRequest
        {
            UserId = user.Id,
            Body = new AddShoppingListRequestBody
            {
                Title = "Test List",
                CreatedFrom = Guid.NewGuid()
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("Post does not exist.");
    }
}
