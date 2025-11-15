using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.DeletePost;

public class DeletePostRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name="postId")]
    public Guid PostId { get; set; }
}