using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

public class UnsavePostRequest
{
    public Guid PostId { get; set; }
    
    [FromBody]
    public Guid UserId { get; set; }    
}
