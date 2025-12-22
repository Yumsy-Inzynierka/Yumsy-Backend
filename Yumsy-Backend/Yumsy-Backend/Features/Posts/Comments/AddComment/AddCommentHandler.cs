using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.Comments.AddComment;

public class AddCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public AddCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddCommentResponse> Handle(AddCommentRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == request.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {request.UserId} not found");
    
        var postExists = await _dbContext.Posts
            .AnyAsync(p => p.Id == request.PostId, cancellationToken);

        if (!postExists)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} not found");
        
        if (request.Body.ParentCommentId.HasValue)
        {
            var parentExists = await _dbContext.Comments
                .AnyAsync(c => c.Id == request.Body.ParentCommentId.Value, cancellationToken);
    
            if (!parentExists)
                throw new KeyNotFoundException(
                    $"Parent comment with ID: {request.Body.ParentCommentId} not found");
        }
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = request.Body.Content,
            PostId = request.PostId,
            ParentCommentId = request.Body.ParentCommentId,
            UserId = request.UserId,
            CommentedDate = DateTime.UtcNow,
        };
        
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new AddCommentResponse
        {
            Id = comment.Id,
            Content = comment.Content,
            CommentedDate = comment.CommentedDate,
            PostId = comment.PostId,
            UserId = comment.UserId,
            ParentCommentId = comment.ParentCommentId
        };
    }
}