using FluentValidation;

namespace Yumsy_Backend.Features.Users.Register;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is mandatory")
            .EmailAddress().WithMessage("Email address is not valid");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is mandatory")
            .MinimumLength(4).WithMessage("Username must be at least 3 characters long")
            .MaximumLength(20).WithMessage("Username must not exceed 20 characters")
            .Matches(@"^[a-zA-Z0-9_.-]*$").WithMessage("Username can only contain letters, digits, underscores, hyphens, and periods");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is mandatory")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
            .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain at least one number")
            .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
    }
}