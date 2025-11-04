using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

public class DeleteShoppingListRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "shoppingListId")]
    public Guid ShoppingListId { get; set; }
}