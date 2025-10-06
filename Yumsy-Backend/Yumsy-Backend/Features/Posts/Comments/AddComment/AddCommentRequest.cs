using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Comments.AddComment;

public class AddCommentRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }

    [FromBody]
    public AddCommentRequestBody Body { get; set; } = default!;
}

public class AddCommentRequestBody
{
    public string Content { get; set; } = string.Empty;
    public Guid? ParentCommentId { get; set; }
}