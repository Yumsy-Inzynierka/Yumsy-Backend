
namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListRequest
{
    public Guid UserId { get; set; }
    public string Title { get; init; }
    public Guid CreatedFrom { get; init; }
}
