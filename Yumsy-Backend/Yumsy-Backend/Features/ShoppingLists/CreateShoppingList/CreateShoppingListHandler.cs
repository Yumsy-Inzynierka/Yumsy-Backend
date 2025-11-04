using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListHandler
{
    private readonly SupabaseDbContext _dbContext;

    public CreateShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateShoppingListResponse> Handle(CreateShoppingListRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var ingredientIds = request.Body.Ingredients.Select(i => i.Id).ToList();

        var existingIngredients = await _dbContext.Ingredients
            .Where(i => ingredientIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync(cancellationToken);

        var missingIngredients = ingredientIds.Except(existingIngredients).ToList();
        if (missingIngredients.Any())
            throw new KeyNotFoundException($"One or more ingredients do not exist: {string.Join(", ", missingIngredients)}");

        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = request.Body.Title,
            UserId = request.UserId,
            IngredientShoppingLists = request.Body.Ingredients
                .Select(i => new IngredientShoppingList
                {
                    ShoppingListId = Guid.NewGuid(),
                    IngredientId = i.Id,
                    Quantity = i.Quantity
                }).ToList()
        };

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            await _dbContext.ShoppingLists.AddAsync(shoppingList, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return new CreateShoppingListResponse
        {
            Id = shoppingList.Id
        };
    }
}
