using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment;

public class LikeCommentRequest
{
    public Guid CommentId { get; set; }
    
    [FromBody]
    public Guid UserId { get; set; }   
}
