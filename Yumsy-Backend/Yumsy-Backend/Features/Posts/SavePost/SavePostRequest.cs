using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.SavePost;

public class SavePostRequest
{
    public Guid UserId { get; set; }   
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
    
}
