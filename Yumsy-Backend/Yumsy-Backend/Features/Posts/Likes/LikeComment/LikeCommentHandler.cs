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

    public async Task Handle(LikeCommentRequest request, CancellationToken cancellationToken)
        {
            var userExists = await _dbContext.Users
                .AsNoTracking()
                .AnyAsync(u => u.Id == request.UserId, cancellationToken);
    
            if (!userExists)
                throw new KeyNotFoundException($"User with ID: {request.UserId} not found.");
    
            var comment = await _dbContext.Comments
                .FirstOrDefaultAsync(c => c.Id == request.CommentId, cancellationToken);
    
            if (comment == null)
                throw new KeyNotFoundException($"Comment with ID: {request.CommentId} not found.");
    
            var alreadyLiked = await _dbContext.CommentLikes
                .AsNoTracking()
                .AnyAsync(l => l.CommentId == request.CommentId && l.UserId == request.UserId, cancellationToken);
    
            if (alreadyLiked)
                throw new InvalidOperationException($"User with ID: {request.UserId} already liked comment with ID: {request.CommentId}.");
    
            var commentLike = new CommentLike
            {
                CommentId = request.CommentId,
                UserId = request.UserId
            };
    
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await _dbContext.CommentLikes.AddAsync(commentLike, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
    
                comment.LikesCount = await _dbContext.CommentLikes
                    .CountAsync(l => l.CommentId == request.CommentId, cancellationToken);
    
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