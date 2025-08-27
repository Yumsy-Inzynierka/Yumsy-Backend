using FluentValidation;

namespace Yumsy_Backend.Features.Comments.DeleteComment;

public class DeleteCommentValidator : AbstractValidator<DeleteCommentRequest>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x);
    }
}