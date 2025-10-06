using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

public class UnsavePostRequest
{
    public Guid UserId { get; set; }  
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
    
}
