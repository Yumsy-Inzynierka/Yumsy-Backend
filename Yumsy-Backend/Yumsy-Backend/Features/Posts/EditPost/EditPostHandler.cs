/*using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.EditPost;
public class EditPostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public EditPostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EditPostResponse> Handle(Guid shoppingListId, 
        EditPostRequest editPostRequest, 
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

        shoppingList.Title = editPostRequest.Title;
        
        _dbContext.RemoveRange(shoppingList.IngredientShoppingLists);
        
        foreach (var ingredient in editPostRequest.Ingredients)
        {
            shoppingList.IngredientShoppingLists.Add(new IngredientShoppingList
            {
                ShoppingListId = shoppingList.Id,
                IngredientId = ingredient.IngredientId,
                Quantity = ingredient.Quantity
            });
        }
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new EditPostResponse
        {
            Id = shoppingList.Id,
            Title = shoppingList.Title,
            Ingredients = editPostRequest.Ingredients
        };

    }
}*/