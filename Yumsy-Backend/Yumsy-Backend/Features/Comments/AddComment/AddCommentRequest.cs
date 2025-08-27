using Microsoft.EntityFrameworkCore.Metadata;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class AddCommentRequest
{
    public Guid PostId { get; set; }
    public Guid UserId { get; set; }
    public string Content { get; set; }
    public Guid? ParenCommentId { get; set; }
}

