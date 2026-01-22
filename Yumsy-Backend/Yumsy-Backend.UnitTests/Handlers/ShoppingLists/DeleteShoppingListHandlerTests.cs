using FluentAssertions;
using Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.ShoppingLists;

public class DeleteShoppingListHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new DeleteShoppingListHandler(context);
        var userId = Guid.NewGuid();
        var request = new DeleteShoppingListRequest
        {
            UserId = userId,
            ShoppingListId = Guid.NewGuid()
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
        var handler = new DeleteShoppingListHandler(context);
        var request = new DeleteShoppingListRequest
        {
            UserId = user.Id,
            ShoppingListId = shoppingListId
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

        var handler = new DeleteShoppingListHandler(context);
        var request = new DeleteShoppingListRequest
        {
            UserId = otherUser.Id,
            ShoppingListId = shoppingList.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage($"User with ID: {otherUser.Id} is not the owner of this shopping list with with ID: {shoppingList.Id}.");
    }
}
