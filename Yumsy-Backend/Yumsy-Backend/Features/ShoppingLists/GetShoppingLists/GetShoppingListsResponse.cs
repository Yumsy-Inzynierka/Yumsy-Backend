namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

public record GetShoppingListsResponse
{
    public IEnumerable<GetShoppingListResponse> ShoppingLists { get; init; }
}

public record GetShoppingListResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string? Username { get; init; }
    public Guid? PostId { get; init; }
    public IEnumerable<GetShoppingListIngredientResponse> Ingredients { get; init; }
}

public record GetShoppingListIngredientResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public int Quantity { get; init; }
}