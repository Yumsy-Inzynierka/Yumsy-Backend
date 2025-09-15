using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListHandler
{
    public SupabaseDbContext _dbContext;

    public AddShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddShoppingListResponse> Handle(AddShoppingListRequest request, Guid userId,
        CancellationToken cancellationToken)
    {
        var postExists = await _dbContext.Posts.AnyAsync(p => p.Id == request.CreatedFrom, cancellationToken);
        if (!postExists)
            throw new KeyNotFoundException("Post specified in CreatedFrom does not exist");
        
        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            UserId = userId,
            CreatedFromId = request.CreatedFrom
        };
        
        foreach (var ingredient in request.Ingredients)
        {
            shoppingList.IngredientShoppingLists.Add(new IngredientShoppingList
            {
                ShoppingListId = shoppingList.Id,
                IngredientId = ingredient.Id,
                Quantity = ingredient.Quantity
            });
        }
        
        _dbContext.ShoppingLists.Add(shoppingList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddShoppingListResponse
        {
            Id = shoppingList.Id,
        };
    }
}