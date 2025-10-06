using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public class EditShoppingListRequest
{
    public Guid UserId { get; set; }

    [FromRoute(Name = "shoppingListId")]
    public Guid ShoppingListId { get; set; }

    [FromBody]
    public EditShoppingListRequestBody Body { get; set; } = default!;
}

public class EditShoppingListRequestBody
{
    public string Title { get; init; } = string.Empty;
    public IEnumerable<EditShoppingListIngredient> Ingredients { get; init; } = Enumerable.Empty<EditShoppingListIngredient>();
}

public class EditShoppingListIngredient
{
    public Guid IngredientId { get; init; }
    public int Quantity { get; init; }
}