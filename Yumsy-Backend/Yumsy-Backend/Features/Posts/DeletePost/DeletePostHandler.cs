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

    public async Task Handle(DeletePostRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId && p.UserId == request.UserId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException(
                $"Post with ID: {request.PostId} for User ID: {request.UserId} not found.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}