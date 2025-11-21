using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetTopDailyPosts;

public class GetTopDailyPostsValidator : AbstractValidator<GetTopDailyPostsRequest>
{
    public GetTopDailyPostsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}