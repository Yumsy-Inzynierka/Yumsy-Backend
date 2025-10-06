using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Comments.DeleteComment;

public class DeleteCommentRequest
{
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
    
    [FromRoute(Name = "commentId")]
    public Guid CommentId { get; set; }
}