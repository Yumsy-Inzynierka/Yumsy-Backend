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

    public async Task Handle(SavePostRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found");

        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");

        var alreadySaved = await _dbContext.Saved
            .AsNoTracking()
            .AnyAsync(s => s.PostId == request.PostId && s.UserId == request.UserId, cancellationToken);

        if (alreadySaved)
            throw new InvalidOperationException("Post is already saved by this user");

        var savedPost = new Saved
        {
            UserId = request.UserId,
            PostId = request.PostId
        };

        await _dbContext.Saved.AddAsync(savedPost, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
