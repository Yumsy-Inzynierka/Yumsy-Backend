using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

public class UnlikeCommentRequest
{
    public Guid UserId { get; set; }   
    
    [FromRoute(Name = "commentId")]
    public Guid CommentId { get; set; }
    
}
