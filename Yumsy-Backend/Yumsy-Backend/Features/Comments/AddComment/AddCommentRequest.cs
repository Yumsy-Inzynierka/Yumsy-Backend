using Microsoft.EntityFrameworkCore.Metadata;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class AddCommentRequest
{
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public Guid? ParentCommentId { get; set; }
}

