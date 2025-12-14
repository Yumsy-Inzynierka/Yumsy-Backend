using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared;

namespace Yumsy_Backend.Features.Users.SearchUsers;

public class SearchUsersHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public SearchUsersHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SearchUsersResponse> Handle(SearchUsersRequest searchUsersRequest, 
        CancellationToken cancellationToken)
    {
        var page = searchUsersRequest.Page;
        var offset = (page - 1) * YumsyConstants.SEARCH_USERS_AMOUNT;
        var query = searchUsersRequest.Query?.Trim().ToLower() ?? string.Empty;
        
        var searchQuery = _dbContext.Users
            .Where(u => u.Username.ToLower().Contains(query) || 
                        u.ProfileName.ToLower().Contains(query))
            .OrderBy(u => u.Username);
        
        var users = await searchQuery
            .Skip(offset)
            .Take(YumsyConstants.SEARCH_USERS_AMOUNT + 1)
            .Select(u => new SearchUserResponse
            {
                Id = u.Id,
                Username = u.Username,
                ProfileName = u.ProfileName,
                ProfilePictureUrl = u.ProfilePicture
            })
            .ToListAsync(cancellationToken);

        var hasMore = users.Count > YumsyConstants.SEARCH_USERS_AMOUNT;
        
        if (hasMore)
        {
            users.RemoveAt(users.Count - 1);
        }

        return new SearchUsersResponse
        {
            Users = users,
            Page = page,
            HasMore = hasMore
        };
    }
}