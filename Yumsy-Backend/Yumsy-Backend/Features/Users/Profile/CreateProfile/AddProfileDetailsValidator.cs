using FluentValidation;

namespace Yumsy_Backend.Features.Users.Profile.CreateProfile;

public class AddProfileDetailsValidator : AbstractValidator<AddProfileDetailsRequest>
{
    public AddProfileDetailsValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("UserId cannot be empty");
        
        RuleFor(x => x.ProfileName)
            .NotEmpty().WithMessage("ProfileName is required.")
            .MaximumLength(20).WithMessage("ProfileName cannot exceed 20 characters.");
            
        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("Bio is required.")
            .MaximumLength(400).WithMessage("Bio cannot exceed 400 characters.");
        
        RuleFor(x => x.ProfilePicture)
            .NotEmpty().WithMessage("ProfilePicture is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("ProfilePicture must be a valid URL.");
    }
}