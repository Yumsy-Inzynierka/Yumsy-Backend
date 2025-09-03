using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.SavePost;

public class SavePostRequest
{
    public Guid PostId { get; set; }
    
    [FromBody]
    public Guid UserId { get; set; }    
}
