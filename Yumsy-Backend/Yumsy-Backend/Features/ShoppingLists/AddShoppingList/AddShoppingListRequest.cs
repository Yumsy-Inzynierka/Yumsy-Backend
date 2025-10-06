
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListRequest
{
    public Guid UserId { get; set; }
    
    [FromBody]
    public AddShoppingListRequestBody Body { get; set; } = default!;
}

public class AddShoppingListRequestBody
{
    public string Title { get; set; } = String.Empty;
    public Guid CreatedFrom { get; set; }
}