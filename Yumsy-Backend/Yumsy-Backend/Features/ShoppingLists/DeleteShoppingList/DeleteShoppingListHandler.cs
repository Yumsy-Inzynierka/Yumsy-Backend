using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

public class DeleteShoppingListHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public DeleteShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteShoppingListRequest deleteShoppingListRequest)
    {
        var shoppingList = await _dbContext.ShoppingLists
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == deleteShoppingListRequest.ShoppingListId);
        
        if (shoppingList is null)
            throw new KeyNotFoundException("shoppingList does not exist");;
        
        _dbContext.ShoppingLists.Remove(shoppingList);
        await _dbContext.SaveChangesAsync();
    }
}