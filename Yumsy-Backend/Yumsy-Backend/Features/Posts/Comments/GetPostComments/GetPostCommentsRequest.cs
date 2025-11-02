using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public class GetPostCommentsRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
}