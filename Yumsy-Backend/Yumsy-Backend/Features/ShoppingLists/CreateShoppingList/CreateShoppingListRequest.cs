
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListRequest
{
    public Guid UserId { get; set; }

    [FromBody]
    public CreateShoppingListRequestBody Body { get; set; } = default!;
}

public class CreateShoppingListRequestBody
{
    public string Title { get; init; } = string.Empty;
    public IEnumerable<AddShoppingListIngredientRequest> Ingredients { get; init; } = Enumerable.Empty<AddShoppingListIngredientRequest>();
}

public class AddShoppingListIngredientRequest
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
}
