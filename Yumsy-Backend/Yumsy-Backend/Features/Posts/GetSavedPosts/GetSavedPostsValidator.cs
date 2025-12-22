using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

public class GetSavedPostsValidator : AbstractValidator<GetSavedPostsRequest>
{
    public GetSavedPostsValidator()
    {
        RuleFor(x => x.CurrentPage)
            .GreaterThan(0).WithMessage("Page must be greater than 0.");
    }
}