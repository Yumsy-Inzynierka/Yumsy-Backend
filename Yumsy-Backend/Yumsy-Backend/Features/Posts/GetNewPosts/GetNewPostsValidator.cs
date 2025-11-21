using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetNewPosts;

public class GetNewPostsValidator : AbstractValidator<GetNewPostsRequest>
{
    public GetNewPostsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}