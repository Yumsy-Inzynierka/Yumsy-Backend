using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.ShoppingLists;

public class CreateShoppingListHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new CreateShoppingListHandler(context);
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = "Test List",
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.NewGuid(), Quantity = 1 } }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {request.UserId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_IngredientDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new CreateShoppingListHandler(context);
        var ingredientId = Guid.NewGuid();
        var request = new CreateShoppingListRequest
        {
            UserId = user.Id,
            Body = new CreateShoppingListRequestBody
            {
                Title = "Test List",
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = ingredientId, Quantity = 1 } }
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"One or more ingredients do not exist: {ingredientId}");
    }

    [Fact]
    public async Task Handle_Should_CreateShoppingList_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var ingredient1 = TestDataBuilder.CreateIngredient();
        var ingredient2 = TestDataBuilder.CreateIngredient(name: "Another Ingredient");

        context.Users.Add(user);
        context.Ingredients.AddRange(ingredient1, ingredient2);
        await context.SaveChangesAsync();

        var handler = new CreateShoppingListHandler(context);
        var request = new CreateShoppingListRequest
        {
            UserId = user.Id,
            Body = new CreateShoppingListRequestBody
            {
                Title = "My Shopping List",
                Ingredients = new[]
                {
                    new AddShoppingListIngredientRequest { Id = ingredient1.Id, Quantity = 2 },
                    new AddShoppingListIngredientRequest { Id = ingredient2.Id, Quantity = 3 }
                }
            }
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();

        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.IngredientShoppingLists)
            .FirstOrDefaultAsync(sl => sl.Id == result.Id);

        shoppingList.Should().NotBeNull();
        shoppingList!.Title.Should().Be("My Shopping List");
        shoppingList.UserId.Should().Be(user.Id);
        shoppingList.IngredientShoppingLists.Should().HaveCount(2);
    }
}
