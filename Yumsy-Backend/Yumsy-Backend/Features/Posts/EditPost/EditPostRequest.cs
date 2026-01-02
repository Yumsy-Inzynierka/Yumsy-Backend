using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.EditPost;

public class EditPostRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
    
    [FromBody]
    public EditPostRequestBody Body { get; set; } = default!;
}

public class EditPostRequestBody
{
    public string Title { get; set; }
    public string Description { get; set; }
}