namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public record GetSavedPostsResponse
{
    public List<GetSavedPostResponse> SavedPosts { get; init; }
    public int CurrentPage { get; init; }
    public bool HasMore { get; set; } = false;
}

public record GetSavedPostResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
}