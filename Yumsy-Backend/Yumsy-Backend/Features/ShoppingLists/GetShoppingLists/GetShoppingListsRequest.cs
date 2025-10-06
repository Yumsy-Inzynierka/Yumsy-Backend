using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

public class GetShoppingListsRequest
{
    public Guid UserId { get; set; }

}
