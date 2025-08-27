using FluentValidation;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

public class GetProfileDetailsValidator : AbstractValidator<GetProfileDetailsRequest>
{
    public GetProfileDetailsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId cannot be empty");
    }
}