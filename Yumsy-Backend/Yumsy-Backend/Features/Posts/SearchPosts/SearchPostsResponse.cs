namespace Yumsy_Backend.Features.Posts.SearchPosts;

public record SearchPostsResponse
{
    public List<SearchPostResponse> Posts { get; init; }
    public int Page { get; init; } = 0;
    public bool HasMore { get; init; } = false;
}

public record SearchPostResponse
{
    public Guid Id { get; init; }
    public string PostTitle { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string ImageUrl { get; init; }
    public int CookingTime { get; init; }
    public float RelevanceScore { get; init; }
}