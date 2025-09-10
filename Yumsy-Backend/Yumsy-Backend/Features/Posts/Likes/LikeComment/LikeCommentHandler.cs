using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment;

public class LikeCommentHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public LikeCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LikeCommentResponse> Handle(LikeCommentRequest likeCommentRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == likeCommentRequest.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {likeCommentRequest.UserId} not found.");
    
        var comment = await _dbContext.Comments
            .FirstOrDefaultAsync(u => u.Id == likeCommentRequest.CommentId, cancellationToken);

        if (comment == null)
            throw new KeyNotFoundException($"Comment with ID: {likeCommentRequest.CommentId} not found.");

        var alreadyLiked = await _dbContext.CommentLikes
            .AnyAsync(l => l.CommentId == likeCommentRequest.CommentId && l.UserId == likeCommentRequest.UserId, cancellationToken);

        if (alreadyLiked)
            throw new InvalidOperationException($"User with Id: {likeCommentRequest.UserId} already liked comment with Id: {likeCommentRequest.CommentId}.");
    
        var commentLike = new CommentLike
        {
            CommentId = likeCommentRequest.CommentId,
            UserId = likeCommentRequest.UserId
        };

        await _dbContext.CommentLikes.AddAsync(commentLike, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        comment.LikesCount = await _dbContext.CommentLikes.CountAsync(l => l.CommentId == likeCommentRequest.CommentId, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new LikeCommentResponse
        {
            commentId = commentLike.CommentId
        };
    }
}