using FluentValidation;

namespace Yumsy_Backend.Features.Comments.AddComment;

public class AddCommentValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(x => x);
    }
}