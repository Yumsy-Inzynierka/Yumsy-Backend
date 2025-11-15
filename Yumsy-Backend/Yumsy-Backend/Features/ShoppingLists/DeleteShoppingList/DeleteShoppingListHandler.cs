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

    public async Task Handle(DeleteShoppingListRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var shoppingList = await _dbContext.ShoppingLists
            .AsTracking()
            .Include(sl => sl.IngredientShoppingLists)
            .FirstOrDefaultAsync(sl => sl.Id == request.ShoppingListId, cancellationToken);

        if (shoppingList is null)
            throw new KeyNotFoundException($"Shopping list with ID: {request.ShoppingListId} not found.");

        if (shoppingList.UserId != request.UserId)
            throw new UnauthorizedAccessException($"User with ID: {request.UserId} is not the owner of this shopping list with with ID: {shoppingList.Id}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _dbContext.IngredientShoppingLists.RemoveRange(shoppingList.IngredientShoppingLists);
            _dbContext.ShoppingLists.Remove(shoppingList);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}