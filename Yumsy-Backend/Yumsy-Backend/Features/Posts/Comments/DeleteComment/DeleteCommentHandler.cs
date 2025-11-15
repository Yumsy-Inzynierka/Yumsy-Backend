using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.Comments.DeleteComment;

public class DeleteCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public DeleteCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found.");

        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(c => c.Id == request.CommentId && c.PostId == request.PostId, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID: {request.CommentId} not found in post with ID: {request.PostId}.");

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync(cancellationToken);

            post.CommentsCount = await _dbContext.Comments.CountAsync(c => c.PostId == request.PostId, cancellationToken);
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