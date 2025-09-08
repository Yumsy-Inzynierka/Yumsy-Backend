using FluentValidation;

namespace Yumsy_Backend.Features.Posts.UnlikePost;

public class UnlikePostValidator : AbstractValidator<UnlikePostRequest>
{
    public UnlikePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .WithMessage("PostId cannot be empty.");
    }
}