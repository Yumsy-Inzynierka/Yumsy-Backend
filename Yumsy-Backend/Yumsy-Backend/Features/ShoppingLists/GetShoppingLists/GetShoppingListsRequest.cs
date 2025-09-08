using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.GetShoppingLists;

public class GetShoppingListsRequest
{
    [FromRoute]
    public Guid UserId { get; set; }
}