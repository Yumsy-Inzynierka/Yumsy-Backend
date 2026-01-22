using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;

public class GetShoppingListsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetShoppingListsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetShoppingListsResponse> Handle(GetShoppingListsRequest getShoppingListsRequest)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == getShoppingListsRequest.UserId);

        if (!userExists)
        {
            throw new KeyNotFoundException(
                $"User with ID: {getShoppingListsRequest.UserId} not found."
            );
        }

        var shoppingLists = await _dbContext.ShoppingLists
            .AsNoTracking()
            .Where(sl => sl.UserId == getShoppingListsRequest.UserId)
            .Include(sl => sl.CreatedFrom)
            .ThenInclude(post => post.CreatedBy)
            .Include(sl => sl.IngredientShoppingLists)
            .ThenInclude(isl => isl.Ingredient)
            .OrderByDescending(sl => sl.Id)
            .ToListAsync();

        var responseLists = shoppingLists.Select(sl => new GetShoppingListResponse
            {
                Id = sl.Id,
                Name = sl.Title,

                PostId = sl.CreatedFrom?.Id,
                Username = sl.CreatedFrom?.CreatedBy?.Username,

                Ingredients = sl.IngredientShoppingLists
                    .Where(isl => isl.Ingredient != null)
                    .Select(isl => new GetShoppingListIngredientResponse
                    {
                        Id = isl.Ingredient!.Id,
                        Name = isl.Ingredient.Name,
                        Quantity = isl.Quantity
                    })
                    .ToList()
            })
            .ToList();

        return new GetShoppingListsResponse
        {
            ShoppingLists = responseLists
        };
    }
}