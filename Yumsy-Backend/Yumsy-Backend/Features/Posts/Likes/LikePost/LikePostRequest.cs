using Microsoft.AspNetCore.Mvc;
using EnvironmentName = Microsoft.AspNetCore.Hosting.EnvironmentName;

namespace Yumsy_Backend.Features.Posts.Likes.LikePost;

public class LikePostRequest
{
    public Guid UserId { get; set; }
    [FromRoute(Name="postId")]
    public Guid PostId { get; set; }
}