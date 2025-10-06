using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Comments.DeleteComment;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentRequest>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.CommentId)
            .NotEmpty().WithMessage("CommentId is required.")
            .NotEqual(Guid.Empty).WithMessage("CommentId must be a valid GUID.");
        
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.") 
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}