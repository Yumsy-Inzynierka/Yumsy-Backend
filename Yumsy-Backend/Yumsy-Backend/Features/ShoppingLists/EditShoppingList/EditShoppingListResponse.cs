namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public record EditShoppingListResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public List<EditShoppingListIngredient> Ingredients { get; init; }
}