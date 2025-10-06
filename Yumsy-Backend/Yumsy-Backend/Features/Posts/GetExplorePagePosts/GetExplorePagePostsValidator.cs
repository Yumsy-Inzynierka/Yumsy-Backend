using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

public class GetExplorePagePostsValidator : AbstractValidator<GetExplorePagePostsRequest>
{
    public GetExplorePagePostsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.CurrentPage)
            .GreaterThan(0).WithMessage("CurrentPage must be greater than 0");
    }
}