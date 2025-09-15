namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public class EditShoppingListRequest
{
    public string Title { get; init; } = null!;
    public List<EditShoppingListIngredient> Ingredients { get; init; } = new();
}


public class EditShoppingListIngredient
{
    public Guid IngredientId { get; init; }
    public int Quantity { get; init; }
}