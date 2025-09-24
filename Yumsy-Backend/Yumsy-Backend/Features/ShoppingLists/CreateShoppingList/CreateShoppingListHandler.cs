using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListHandler
{
    public SupabaseDbContext _dbContext;

    public CreateShoppingListHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateShoppingListResponse> Handle(CreateShoppingListRequest request, CancellationToken cancellationToken)
    {
        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            UserId = request.UserId,
            CreatedFromId = request.UserId
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

        return new CreateShoppingListResponse
        {
            Id = shoppingList.Id,
        };
    }
}