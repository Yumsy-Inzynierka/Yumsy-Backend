using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

public class UnsavePostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public UnsavePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnsavePostRequest unsavePostRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == unsavePostRequest.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {unsavePostRequest.UserId} not found.");
    
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == unsavePostRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {unsavePostRequest.PostId} not found.");

        var savedPost = await _dbContext.Saved
            .FirstOrDefaultAsync(l => l.PostId == unsavePostRequest.PostId && l.UserId == unsavePostRequest.UserId, cancellationToken);

        if (savedPost == null)
            throw new InvalidOperationException("Post is already unsaved by this user.");
        
        _dbContext.Saved.Remove(savedPost);
        post.SavedCount = _dbContext.Saved.Count(l => l.PostId == savedPost.PostId);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}