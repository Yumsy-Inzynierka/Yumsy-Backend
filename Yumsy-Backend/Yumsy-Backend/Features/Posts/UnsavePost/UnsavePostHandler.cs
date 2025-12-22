using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

public class UnsavePostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public UnsavePostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnsavePostRequest request, CancellationToken cancellationToken)
    {
        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var savedPost = await _dbContext.Saved
            .FirstOrDefaultAsync(
                s => s.PostId == request.PostId && s.UserId == request.UserId, 
                cancellationToken);

        if (savedPost == null)
            throw new InvalidOperationException("Post is not saved by this user");

        _dbContext.Saved.Remove(savedPost);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}