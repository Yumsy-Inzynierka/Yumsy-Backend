namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;
public record GetExplorePagePostsResponse
{
    public List<GetExplorePagePostResponse> Posts { get; set; }
    public int CurrentPage { get; set; }
    public bool HasMore { get; init; } = false;
}

public record GetExplorePagePostResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
}