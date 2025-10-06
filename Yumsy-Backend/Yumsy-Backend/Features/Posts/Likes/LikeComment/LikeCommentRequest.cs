using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment;

public class LikeCommentRequest
{
    public Guid UserId { get; set; }   
    
    [FromForm(Name = "commentId")]
    public Guid CommentId { get; set; }
}
