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

    public async Task Handle(DeleteCommentRequest deleteCommentRequest,CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .Include(p => p.Comments)
            .FirstOrDefaultAsync(c => c.Id == deleteCommentRequest.PostId, cancellationToken);
        
        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {deleteCommentRequest.PostId} not found.");
        
        var comment = post.Comments.FirstOrDefault(c => c.Id == deleteCommentRequest.CommentId);
        
        if(comment == null)
            throw new KeyNotFoundException($"Comment with ID: {deleteCommentRequest.CommentId} not found in post with Id: {deleteCommentRequest.PostId}.");
        
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        comment.Post.CommentsCount = await _dbContext.Comments.CountAsync(l => l.PostId == deleteCommentRequest.PostId);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}