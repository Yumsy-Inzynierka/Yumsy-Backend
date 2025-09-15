using System.ComponentModel.DataAnnotations;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListRequest
{
    [MaxLength(50)]
    public string Title { get; init; }
    public Guid CreatedFrom { get; init; }
    public List<AddShoppingListIngredientRequest> Ingredients { get; init; } = new();
}

public class AddShoppingListIngredientRequest
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
}