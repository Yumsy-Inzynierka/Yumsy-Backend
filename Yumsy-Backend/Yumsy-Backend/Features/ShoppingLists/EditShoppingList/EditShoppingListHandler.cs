using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public class EditShoppingListHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public EditShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EditShoppingListResponse> Handle(EditShoppingListRequest editShoppingListRequest, CancellationToken cancellationToken)
    {
        var shoppingList = await _dbContext.ShoppingLists
            .Include(sl => sl.IngredientShoppingLists)
            .FirstOrDefaultAsync(sl => sl.Id == editShoppingListRequest.ShoppingListId && sl.UserId == editShoppingListRequest.UserId, cancellationToken);

        if (shoppingList == null)
        {
            throw new KeyNotFoundException("Shopping list not found or access denied");
        }

        shoppingList.Title = editShoppingListRequest.Body.Title;
        
        _dbContext.RemoveRange(shoppingList.IngredientShoppingLists);
        
        foreach (var ingredient in editShoppingListRequest.Body.Ingredients)
        {
            shoppingList.IngredientShoppingLists.Add(new IngredientShoppingList
            {
                ShoppingListId = shoppingList.Id,
                IngredientId = ingredient.IngredientId,
                Quantity = ingredient.Quantity
            });
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new EditShoppingListResponse
        {
            Id = shoppingList.Id,
            Title = shoppingList.Title,
            Ingredients = editShoppingListRequest.Body.Ingredients
        };

    }
}