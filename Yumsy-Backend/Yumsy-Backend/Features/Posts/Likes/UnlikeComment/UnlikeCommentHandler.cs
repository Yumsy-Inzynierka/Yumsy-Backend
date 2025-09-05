using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

public class UnlikeCommentHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public UnlikeCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UnlikeCommentRequest unlikeCommentRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == unlikeCommentRequest.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {unlikeCommentRequest.UserId} not found.");
    
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(u => u.Id == unlikeCommentRequest.CommentId, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID: {unlikeCommentRequest.CommentId} not found.");

        var likedComment = await _dbContext.CommentLikes
            .FirstOrDefaultAsync(l => l.CommentId == unlikeCommentRequest.CommentId && l.UserId == unlikeCommentRequest.UserId, cancellationToken);

        if (likedComment == null)
            throw new InvalidOperationException($"User with Id: {unlikeCommentRequest.UserId} already does not like comment with Id: {unlikeCommentRequest.CommentId}.");

        comment.LikesCount -= 1;

        _dbContext.CommentLikes.Remove(likedComment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}