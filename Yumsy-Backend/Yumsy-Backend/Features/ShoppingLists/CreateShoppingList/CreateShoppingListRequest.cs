
namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListRequest
{
    public Guid UserId { get; set; }
    public string Title { get; init; }
    public List<AddShoppingListIngredientRequest> Ingredients { get; init; } = new();
}

public class AddShoppingListIngredientRequest
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
}