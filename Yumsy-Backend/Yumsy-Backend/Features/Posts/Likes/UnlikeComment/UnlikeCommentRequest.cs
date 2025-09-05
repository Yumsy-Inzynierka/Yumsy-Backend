using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

public class UnlikeCommentRequest
{
    public Guid CommentId { get; set; }
    
    [FromBody]
    public Guid UserId { get; set; }   
}
