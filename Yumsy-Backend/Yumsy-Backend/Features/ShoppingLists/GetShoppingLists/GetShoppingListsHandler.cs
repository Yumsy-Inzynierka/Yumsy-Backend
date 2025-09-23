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
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == getShoppingListsRequest.UserId);
        if (user == null)
        {
            throw new KeyNotFoundException("User does not exist");
        }
        
        var shoppingLists = await _dbContext.ShoppingLists
            .AsNoTracking()
            .Where((u => u.UserId == getShoppingListsRequest.UserId))
            .Include(si => si.CreatedFrom)
            .ThenInclude(post => post.CreatedBy)
            .Include(si => si.IngredientShoppingLists)
            .ThenInclude(si => si.Ingredient)
            .ToListAsync();
        
        return new GetShoppingListsResponse
        {
            ShoppingLists = shoppingLists.Select(sl => new GetShoppingListResponse{
                Id = sl.Id,
                Name = sl.Title,
                Username = sl.CreatedFrom.CreatedBy.Username,
                PostId = sl.CreatedFrom.Id,
                Ingredients = sl.IngredientShoppingLists.Select(isl => new GetShoppingListIngredientResponse
                {
                    Id = isl.Ingredient.Id,
                    Name = isl.Ingredient.Name,
                    Quantity = isl.Quantity
                }).ToList()
            })
        };
    }
}