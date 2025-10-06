using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.DeletePost;

public class DeletePostRequest
{
    [FromRoute(Name="postId")]
    public Guid PostId { get; set; }
}