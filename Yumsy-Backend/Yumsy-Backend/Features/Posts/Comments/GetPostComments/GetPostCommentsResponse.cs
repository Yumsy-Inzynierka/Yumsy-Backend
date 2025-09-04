namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public record GetPostCommentsResponse
{
    public List<GetPostCommentResponse> Comments { get; init; }
    
}


public record GetPostCommentResponse
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public DateTime CommentedDate { get; init; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string? UserProfilePictureUrl { get; set; }
    public int LikesCount { get; set; }
    public Guid? ParentCommentId { get; set; }
    public List<GetPostCommentResponse>? ChildComments { get; set; }
}