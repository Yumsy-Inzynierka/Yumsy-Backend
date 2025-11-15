namespace Yumsy_Backend.Features.Posts.GetTopDailyPosts;

public record GetTopDailyPostsResponse
{
    public List<GetTopDailyPostResponse> Posts { get; init; }
}

public record GetTopDailyPostResponse
{
    public Guid Id { get; init; }
    public string PostTitle { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string Image { get; init; }
    public DateTime TimePosted { get; init; }
    public bool IsLiked { get; init; }
    public int Count { get; init; }
    
}