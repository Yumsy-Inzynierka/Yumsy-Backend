using FluentValidation;

namespace Yumsy_Backend.Features.Posts.DeletePost;

public class DeletePostValidator : AbstractValidator<DeletePostRequest>
{
    public DeletePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}