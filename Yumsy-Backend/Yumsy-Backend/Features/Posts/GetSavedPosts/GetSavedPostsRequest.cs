using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public class GetSavedPostsRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute] public uint CurrentPage { get; set; }
}