namespace Yumsy_Backend.Features.Posts.Likes.UnlikePost;

public record UnlikePostResponse
{
    public Guid Id { get; init; }
    public bool Liked { get; init; }
    public int LikesCount { get; init; }
}