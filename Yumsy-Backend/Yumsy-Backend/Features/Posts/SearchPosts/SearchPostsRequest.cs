using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.SearchPosts;

public class SearchPostsRequest
{
    [FromQuery]
    public string Query { get; init; }

    [FromQuery]
    public int Page { get; init; } = 1;
}