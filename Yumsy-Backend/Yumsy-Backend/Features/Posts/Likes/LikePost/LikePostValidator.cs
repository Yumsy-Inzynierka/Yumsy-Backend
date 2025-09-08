using FluentValidation;

namespace Yumsy_Backend.Features.Posts.LikePost;

public class LikePostValidator : AbstractValidator<LikePostRequest>
{
    public LikePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty()
            .WithMessage("PostId cannot be empty.");
    }
}