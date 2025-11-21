using FluentValidation;

namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

public class EditProfileDetailsValidator : AbstractValidator<EditProfileDetailsRequest>
{
    public EditProfileDetailsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new EditProfileDetailsRequestBodyValidator());
    }
}

public class EditProfileDetailsRequestBodyValidator : AbstractValidator<EditProfileDetailsRequestBody>
{
    public EditProfileDetailsRequestBodyValidator()
    {
        RuleFor(x => x.ProfileName)
            .MaximumLength(20).WithMessage("ProfileName cannot exceed 20 characters.");

        RuleFor(x => x.Bio)
            .MaximumLength(400).WithMessage("Bio cannot exceed 400 characters.");

        RuleFor(x => x.ProfilePicture)
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("ProfilePicture must be a valid URL.");
    }
}