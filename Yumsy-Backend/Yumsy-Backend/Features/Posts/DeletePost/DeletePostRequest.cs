using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.DeletePost;

public class DeletePostRequest
{
    [FromRoute]
    public Guid PostId { get; set; }
}