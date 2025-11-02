namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public record GetPostCommentsResponse
{
    public List<GetPostCommentResponse> Comments { get; init; } = new();
}


public record GetPostCommentResponse
{
    public Guid Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public bool IsLiked { get; set; }
    public DateTime CommentedDate { get; init; }
    public Guid UserId { get; init; }
    public string Username { get; init; } = string.Empty;
    public string? UserProfilePictureUrl { get; init; }
    public int LikesCount { get; init; }
    public Guid? ParentCommentId { get; init; }
    public List<GetPostCommentResponse> ChildComments { get; init; } = new();
}