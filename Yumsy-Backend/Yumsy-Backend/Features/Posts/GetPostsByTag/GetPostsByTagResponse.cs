namespace Yumsy_Backend.Features.Posts.GetPostsByTag;

public record GetPostsByTagResponse
{
    public List<GetPostByTagResponse> Posts { get; init; }
    public int CurrentPage { get; init; }
    public bool HasMore { get; init; }
}

public record GetPostByTagResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
}