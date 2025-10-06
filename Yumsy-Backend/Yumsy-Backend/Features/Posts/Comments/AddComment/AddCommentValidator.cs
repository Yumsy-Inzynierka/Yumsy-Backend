using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Comments.AddComment;

public class AddCommentValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new AddCommentRequestBodyValidator());
    }
}

public class AddCommentRequestBodyValidator : AbstractValidator<AddCommentRequestBody>
{
    public AddCommentRequestBodyValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(400).WithMessage("Content cannot exceed 400 characters.");

        RuleFor(c => c.ParentCommentId)
            .Must(id => id == null || id != Guid.Empty)
            .WithMessage("ParentCommentId must be a valid GUID or null.");
    }
}