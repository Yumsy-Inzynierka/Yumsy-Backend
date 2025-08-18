using Yumsy_Backend.Features.Posts.GetPostDetails;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.GetShoppingLists;

public class GetShoppingListsHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public GetShoppingListsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetShoppingListsResponse> Handle(GetShoppingListsRequest getShoppingListsRequest)
    {
        //czy istnieje taki userId
        
        return new GetShoppingListsResponse
        {
            
        };
    }
}