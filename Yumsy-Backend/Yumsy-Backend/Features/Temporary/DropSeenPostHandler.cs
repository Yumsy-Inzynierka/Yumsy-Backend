using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Temporary.DropSeenPost;

public class DropSeenPostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public DropSeenPostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DropSeenPostRequest request)
    {
        var seenPosts = await _dbContext.SeenPosts
            .Where(sp => sp.UserId == request.UserId)
            .ToListAsync();

        _dbContext.SeenPosts.RemoveRange(seenPosts);
        await _dbContext.SaveChangesAsync();
    }
}