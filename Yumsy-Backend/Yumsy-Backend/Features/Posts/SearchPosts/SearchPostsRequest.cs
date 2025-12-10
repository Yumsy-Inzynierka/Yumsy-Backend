using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.SearchPosts;

public class SearchPostsRequest
{
    [FromQuery(Name = "query")]
    public string Query { get; init; }

    [FromQuery(Name = "page")]
    public int Page { get; init; } = 1;
}