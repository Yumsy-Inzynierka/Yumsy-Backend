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

    public async Task<EditShoppingListResponse> Handle(Guid shoppingListId, 
        EditShoppingListRequest editShoppingListRequest, 
        Guid userId, 
        CancellationToken cancellationToken)
    {
        var shoppingList = await _dbContext.ShoppingLists
            .Include(sl => sl.IngredientShoppingLists)
            .FirstOrDefaultAsync(sl => sl.Id == shoppingListId && sl.UserId == userId, cancellationToken);

        if (shoppingList == null)
        {
            throw new KeyNotFoundException("Shopping list not found or access denied");
        }

        shoppingList.Title = editShoppingListRequest.Title;
        
        _dbContext.RemoveRange(shoppingList.IngredientShoppingLists);
        
        foreach (var ingredient in editShoppingListRequest.Ingredients)
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
            Ingredients = editShoppingListRequest.Ingredients
        };

    }
}