using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.SavePost;

public class SavePostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public SavePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SavePostResponse> Handle(SavePostRequest savePostRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == savePostRequest.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {savePostRequest.UserId} not found.");
    
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == savePostRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {savePostRequest.PostId} not found.");

        var alreadySaved = await _dbContext.Saved
            .AnyAsync(l => l.PostId == savePostRequest.PostId && l.UserId == savePostRequest.UserId, cancellationToken);

        if (alreadySaved)
            throw new InvalidOperationException("Post is already saved by this user.");
    
        var savedPost = new Saved
        {
            UserId = savePostRequest.UserId,
            PostId = savePostRequest.PostId
        };

        await _dbContext.Saved.AddAsync(savedPost, cancellationToken);
        post.SavedCount = _dbContext.Saved.Count(l => l.PostId == savedPost.PostId);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SavePostResponse
        {
            PostId = savedPost.PostId
        };
    }
}