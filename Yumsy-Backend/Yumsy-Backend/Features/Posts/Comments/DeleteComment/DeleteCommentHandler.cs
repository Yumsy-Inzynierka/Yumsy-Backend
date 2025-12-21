using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

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
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(
                c => c.Id == request.CommentId 
                     && c.PostId == request.PostId
                     && c.UserId == request.UserId,  // 👈 Sprawdzamy ownership
                cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID: {request.CommentId} not found or no permissions to delete it");
        
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}