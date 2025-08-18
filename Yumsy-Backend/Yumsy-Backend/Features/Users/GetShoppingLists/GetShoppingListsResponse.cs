namespace Yumsy_Backend.Features.Users.GetShoppingLists;

public class GetShoppingListsResponse
{
    IEnumerable<GetShoppingListResponse> shoppingLists;
}

public class GetShoppingListResponse
{
    public string ShoppingListName { get; set; }
    public Guid? PostId { get; set; } // tutaj to jako link do posta ale nwm czy w ten sposób to rozwiązać 
    private IEnumerable<GetShoppingListIngredientResponse> shoppingListIngredient;
}

public class GetShoppingListIngredientResponse
{
    public string Ingredient { get; set; }
    public int Quantity { get; set; }
}