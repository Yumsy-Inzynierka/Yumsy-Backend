namespace Yumsy_Backend.Features.Posts.Likes.LikePost;

public record LikePostResponse
{
    public Guid Id { get; init; }
    public bool Liked { get; init; }
    public int LikesCount { get; init; }
}