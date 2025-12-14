using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public class GetSavedPostsRequest
{
    public Guid UserId { get; set; }

    [FromQuery(Name = "page")] public int CurrentPage { get; set; } = 1;
}