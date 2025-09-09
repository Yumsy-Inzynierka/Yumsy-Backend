using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class AddCommentHandler
{
    private readonly SupabaseDbContext _dbContext;

    public AddCommentHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddCommentResponse> Handle(AddCommentRequest addCommentRequest, Guid userId, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == userId, cancellationToken);

        if (!userExists)
            throw new KeyNotFoundException($"User with ID: {userId} not found.");
    
        var post = await _dbContext.Posts
            .FirstOrDefaultAsync(u => u.Id == addCommentRequest.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {addCommentRequest.PostId} not found.");
        
        var comment = new Comment
        {
            Id = Guid.NewGuid(),
            Content = addCommentRequest.Content,
            PostId = addCommentRequest.PostId,
            ParentCommentId = addCommentRequest.ParentCommentId,
            UserId = userId,
            CommentedDate = DateTime.UtcNow,
        };
        
        
        _dbContext.Comments.Add(comment);
        post.CommentsCount = _dbContext.Comments.Count(l => l.PostId == addCommentRequest.PostId);
            
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