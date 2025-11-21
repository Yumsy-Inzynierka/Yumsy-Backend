using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public class GetSavedPostsValidator : AbstractValidator<GetSavedPostsRequest>
{
    public GetSavedPostsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}