namespace Yumsy_Backend.Features.Posts.GetNewPosts;

public record GetNewPostsResponse
{
    public List<GetNewPostResponse> Posts { get; init; }
}

public record GetNewPostResponse
{
    public Guid Id { get; init; }
    public string PostTitle { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string Image { get; init; }
    public DateTime TimePosted { get; init; }
    public bool IsLiked { get; init; }
}