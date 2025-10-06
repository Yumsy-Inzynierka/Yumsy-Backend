using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

public class DeleteShoppingListRequest
{
    [FromRoute(Name = "shoppingListId")]
    public Guid ShoppingListId { get; set; }
}