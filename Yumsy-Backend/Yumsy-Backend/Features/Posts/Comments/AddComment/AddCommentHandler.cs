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
        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Content = addCommentRequest.Content,
            PostId = addCommentRequest.PostId,
            ParentCommentId = addCommentRequest.ParentCommentId,
            UserId = userId,
            CommentedDate = DateTime.UtcNow,
        };
        
        _dbContext.Comments.Add(comment);
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