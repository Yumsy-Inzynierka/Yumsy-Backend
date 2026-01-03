using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.EditPost;

public class EditPostHandler
{
    private readonly SupabaseDbContext _dbContext;

    public EditPostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(EditPostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");

        if (post.UserId != request.UserId)
            throw new UnauthorizedAccessException($"User with ID: {request.UserId} is not the owner of this post.");

        post.Title = request.Body.Title;
        post.Description = request.Body.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}