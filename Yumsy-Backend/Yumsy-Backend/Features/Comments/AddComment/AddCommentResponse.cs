namespace Yumsy_Backend.Features.Comments.AddComment;

public record AddCommentResponse
{
    public Guid Id { get; init; }
    public Guid PostId { get; init; }
    public Guid UserId { get; init; }
    public string Content { get; init; }
    public DateTime CommentedDate { get; init; }
    public Guid? ParentCommentId { get; init; }
}