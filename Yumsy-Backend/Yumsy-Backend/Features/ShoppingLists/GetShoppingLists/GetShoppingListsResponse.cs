namespace Yumsy_Backend.Features.Users.GetShoppingLists;

public class GetShoppingListsResponse
{
    public IEnumerable<GetShoppingListResponse> ShoppingLists { get; init; }
}

public class GetShoppingListResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Username { get; init; }
    public Guid PostId { get; set; }
    public IEnumerable<GetShoppingListIngredientResponse> Ingredients { get; init; }
}

public class GetShoppingListIngredientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
}