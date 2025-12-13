namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;
public record GetExplorePagePostsResponse
{
    public IEnumerable<GetExplorePagePostResponse> Posts { get; set; }
}

public record GetExplorePagePostResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
}