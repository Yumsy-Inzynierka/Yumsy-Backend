using Supabase.Gotrue;
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
        var shoppingList = new ShoppingList
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            UserId = userId,
            CreatedFromId = request.CreatedFrom
        };
        
        _dbContext.ShoppingLists.Add(shoppingList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new AddShoppingListResponse
        {
            Id = shoppingList.Id,
        };
    }
}