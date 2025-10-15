using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
}