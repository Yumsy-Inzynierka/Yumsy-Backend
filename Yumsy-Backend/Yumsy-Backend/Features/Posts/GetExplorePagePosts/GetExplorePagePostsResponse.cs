namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;
public record GetExplorePagePostsResponse
{
    public IEnumerable<GetExplorePagePostResponse> Posts { get; set; }
}

public record GetExplorePagePostResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
    public int CookingTime { get; init; }
    public IEnumerable<GetExplorePagePostTagResponse> Tags { get; init; }
}

public record GetExplorePagePostTagResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}

public record GetExplorePagePostResponseDTO
{
    public Guid Id { get; init; }
}