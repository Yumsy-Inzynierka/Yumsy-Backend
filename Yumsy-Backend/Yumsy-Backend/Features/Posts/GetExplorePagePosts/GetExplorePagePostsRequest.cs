using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

public class GetExplorePagePostsRequest
{
    public Guid UserId { get; set; }
}