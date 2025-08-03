using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetHomeFeed;

public class GetHomeFeedForUserValidator : AbstractValidator<GetHomeFeedForUserRequest>
{
    public GetHomeFeedForUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required");
    }
}