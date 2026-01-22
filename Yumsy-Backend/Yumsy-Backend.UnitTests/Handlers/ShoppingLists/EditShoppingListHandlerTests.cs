using FluentAssertions;
using Yumsy_Backend.Features.ShoppingLists.EditShoppingList;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.ShoppingLists;

public class EditShoppingListHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new EditShoppingListHandler(context);
        var userId = Guid.NewGuid();
        var request = new EditShoppingListRequest
        {
            UserId = userId,
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated List",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 1 } }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_ShoppingListDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var shoppingListId = Guid.NewGuid();
        var handler = new EditShoppingListHandler(context);
        var request = new EditShoppingListRequest
        {
            UserId = user.Id,
            ShoppingListId = shoppingListId,
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated List",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 1 } }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Shopping list with ID: {shoppingListId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowUnauthorizedAccessException_When_UserIsNotOwner()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var otherUser = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var shoppingList = TestDataBuilder.CreateShoppingList(userId: user.Id, createdFromId: post.Id);

        context.Users.AddRange(user, otherUser);
        context.Posts.Add(post);
        context.ShoppingLists.Add(shoppingList);
        await context.SaveChangesAsync();

        var handler = new EditShoppingListHandler(context);
        var request = new EditShoppingListRequest
        {
            UserId = otherUser.Id,
            ShoppingListId = shoppingList.Id,
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated List",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 1 } }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage($"User with ID: {otherUser.Id} is not the owner of shopping list with ID: {shoppingList.Id}.");
    }
}
