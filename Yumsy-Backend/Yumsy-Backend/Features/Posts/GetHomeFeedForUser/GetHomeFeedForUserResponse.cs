namespace Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

public record GetHomeFeedForUserResponse
{
    public List<GetHomeFeedForUserPostResponse> Posts { get; init; }
    
}
public record GetHomeFeedForUserPostResponse
{
    public Guid Id { get; init; }
    public string PostTitle { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; }
    public string Image { get; init; }
    public DateTime TimePosted { get; init; }
    public int LikesCount { get; init; }
    public int CommentsCount { get; init; }
    public bool IsLiked { get; init; }
}