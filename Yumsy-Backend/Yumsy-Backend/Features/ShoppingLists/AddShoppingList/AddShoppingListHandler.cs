using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListHandler
{
    private readonly SupabaseDbContext _dbContext;

    public AddShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddShoppingListResponse> Handle(AddShoppingListRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");

        var sourcePost = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.IngredientPosts)
            .ThenInclude(ip => ip.Ingredient)
            .FirstOrDefaultAsync(p => p.Id == request.Body.CreatedFrom, cancellationToken);

        if (sourcePost == null)
            throw new KeyNotFoundException("Post does not exist.");

        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = request.Body.Title,
            UserId = request.UserId,
            CreatedFromId = request.Body.CreatedFrom,
            IngredientShoppingLists = sourcePost.IngredientPosts
                .Select(ip => new IngredientShoppingList
                {
                    ShoppingListId = Guid.NewGuid(),
                    IngredientId = ip.Ingredient.Id,
                    Quantity = ip.Quantity
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

        return new AddShoppingListResponse
        {
            Id = shoppingList.Id
        };
    }
}
