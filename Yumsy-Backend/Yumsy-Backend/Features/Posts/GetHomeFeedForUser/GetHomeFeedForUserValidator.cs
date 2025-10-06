using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

public class GetHomeFeedForUserValidator : AbstractValidator<GetHomeFeedForUserRequest>
{
    public GetHomeFeedForUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}