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

    public async Task<EditShoppingListResponse> Handle(EditShoppingListRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var shoppingList = await _dbContext.ShoppingLists
            .Include(sl => sl.IngredientShoppingLists)
            .FirstOrDefaultAsync(sl => sl.Id == request.ShoppingListId, cancellationToken);

        if (shoppingList is null)
            throw new KeyNotFoundException($"Shopping list with ID: {request.ShoppingListId} not found.");

        if (shoppingList.UserId != request.UserId)
            throw new UnauthorizedAccessException($"User with ID: {request.UserId} is not the owner of shopping list with ID: {request.ShoppingListId}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            shoppingList.Title = request.Body.Title;

            _dbContext.IngredientShoppingLists.RemoveRange(shoppingList.IngredientShoppingLists);

            var newIngredients = request.Body.Ingredients.Select(ingredient => new IngredientShoppingList
            {
                ShoppingListId = shoppingList.Id,
                IngredientId = ingredient.IngredientId,
                Quantity = ingredient.Quantity
            }).ToList();

            shoppingList.IngredientShoppingLists = newIngredients;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return new EditShoppingListResponse
            {
                Id = shoppingList.Id,
                Title = shoppingList.Title,
                Ingredients = request.Body.Ingredients
            };
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
