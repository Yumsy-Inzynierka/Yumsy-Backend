using System.ComponentModel.DataAnnotations;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListRequest
{
    [MaxLength(50)]
    public string Title { get; init; }
    public Guid CreatedFrom { get; init; }
}