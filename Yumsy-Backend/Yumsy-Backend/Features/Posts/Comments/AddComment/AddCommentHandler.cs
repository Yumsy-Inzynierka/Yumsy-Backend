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

    public async Task<AddCommentResponse> Handle(AddCommentRequest addCommentRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == addCommentRequest.UserId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {addCommentRequest.UserId} not found.");
    
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(u => u.Id == addCommentRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {addCommentRequest.PostId} not found.");
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = addCommentRequest.Body.Content,
            PostId = addCommentRequest.PostId,
            ParentCommentId = addCommentRequest.Body.ParentCommentId,
            UserId = addCommentRequest.UserId,
            CommentedDate = DateTime.UtcNow,
        };
        
        _dbContext.Comments.Add(comment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        post.CommentsCount = await _dbContext.Comments.CountAsync(l => l.PostId == addCommentRequest.PostId, cancellationToken);
            
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new AddCommentResponse()
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