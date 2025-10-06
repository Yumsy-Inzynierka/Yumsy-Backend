using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikePost;

public class UnlikePostRequest
{
    public Guid UserId { get; set; }
    [FromRoute(Name =  "postId")]
    public Guid PostId { get; set; }
}