using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Likes.LikePost;

public class LikePostValidator : AbstractValidator<LikePostRequest>
{
    public LikePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}