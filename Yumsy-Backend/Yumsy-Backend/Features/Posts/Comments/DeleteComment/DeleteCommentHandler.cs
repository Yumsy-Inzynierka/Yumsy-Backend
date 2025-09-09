using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Comments.DeleteComment;

public class DeleteCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public DeleteCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteCommentRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var comment = await _dbContext.Comments
            .Include(c => c.User)
            .Include(p => p.Post)
            .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
        
        if (comment == null)
            throw new KeyNotFoundException("Comment does not exist");
        
        /*if (comment.UserId != userId || comment.User.Role != "admin")
            throw new UnauthorizedAccessException("Current user does not have permission to delete comment");*/
        
        _dbContext.Comments.Remove(comment);
        comment.Post.CommentsCount = _dbContext.Comments.Count(l => l.PostId == comment.PostId);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}