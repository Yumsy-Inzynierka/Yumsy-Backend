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

    public async Task<CreateShoppingListResponse> Handle(CreateShoppingListRequest createShoppingListRequest, CancellationToken cancellationToken)
    {
        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = createShoppingListRequest.Body.Title,
            UserId = createShoppingListRequest.UserId,
            CreatedFromId = createShoppingListRequest.UserId
        };
        
        foreach (var ingredient in createShoppingListRequest.Body.Ingredients)
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