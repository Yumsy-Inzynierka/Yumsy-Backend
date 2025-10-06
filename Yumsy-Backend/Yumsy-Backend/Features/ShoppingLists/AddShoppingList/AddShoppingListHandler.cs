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

    public async Task<AddShoppingListResponse> Handle(AddShoppingListRequest addShoppingListRequest, CancellationToken cancellationToken)
    {
        var sourcePost = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.IngredientPosts)
            .ThenInclude(ip => ip.Ingredient)
            .FirstOrDefaultAsync(p => p.Id == addShoppingListRequest.Body.CreatedFrom, cancellationToken);
            
        if (sourcePost == null)
            throw new KeyNotFoundException("Post specified in CreatedFrom does not exist");

        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = addShoppingListRequest.Body.Title,
            UserId = addShoppingListRequest.UserId,
            CreatedFromId = addShoppingListRequest.Body.CreatedFrom,
            IngredientShoppingLists = new List<IngredientShoppingList>()
        };

        foreach (var ingredientPost in sourcePost.IngredientPosts)
        {
            shoppingList.IngredientShoppingLists.Add(new IngredientShoppingList
            {
                ShoppingListId = shoppingList.Id,
                IngredientId = ingredientPost.Ingredient.Id,
                Quantity = ingredientPost.Quantity
            });
        }

        _dbContext.ShoppingLists.Add(shoppingList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddShoppingListResponse
        {
            Id = shoppingList.Id
        };
    }
}