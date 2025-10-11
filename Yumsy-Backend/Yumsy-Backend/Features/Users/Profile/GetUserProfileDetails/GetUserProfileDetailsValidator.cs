using FluentValidation;

namespace Yumsy_Backend.Features.Users.Profile.GetUserProfileDetails;

public class GetUserProfileDetailsValidator : AbstractValidator<GetUserProfileDetailsRequest>
{
    public GetUserProfileDetailsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}