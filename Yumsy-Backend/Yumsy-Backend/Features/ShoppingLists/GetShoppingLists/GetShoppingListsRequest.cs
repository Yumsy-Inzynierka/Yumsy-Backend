using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

public class GetShoppingListsRequest
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }

}
