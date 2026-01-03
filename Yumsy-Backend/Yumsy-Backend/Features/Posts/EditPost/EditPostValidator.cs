using FluentValidation;

namespace Yumsy_Backend.Features.Posts.EditPost;

public class EditPostValidator : AbstractValidator<EditPostRequest>
{
    public EditPostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
        
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new EditPostRequestBodyValidator());
    }
}

public class EditPostRequestBodyValidator : AbstractValidator<EditPostRequestBody>
{
    public EditPostRequestBodyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(400).WithMessage("Description cannot exceed 400 characters.");
    }
}