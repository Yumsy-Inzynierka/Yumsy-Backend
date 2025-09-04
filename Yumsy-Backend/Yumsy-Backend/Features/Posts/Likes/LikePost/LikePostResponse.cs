namespace Yumsy_Backend.Features.Posts.LikePost;

public record LikePostResponse
{
    public Guid Id { get; init; }
    public bool Liked { get; init; }
    public int LikesCount { get; init; }
}