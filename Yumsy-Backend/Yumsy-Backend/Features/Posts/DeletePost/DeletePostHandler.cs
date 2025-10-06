using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.DeletePost;

public class DeletePostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public DeletePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeletePostRequest deletePostRequest)
    {
        var post = await _dbContext.Posts
            .AsTracking()
            .FirstOrDefaultAsync(p => p.Id == deletePostRequest.PostId);
        
        if (post is null)
            throw new KeyNotFoundException($"Post with ID: {deletePostRequest.PostId} does not exist");
        
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}