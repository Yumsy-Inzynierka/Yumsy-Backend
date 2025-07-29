using FluentValidation;

namespace Yumsy_Backend.Features.Users.Login;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email jest wymagany.")
            .EmailAddress().WithMessage("Nieprawidłowy format emaila.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło jest wymagane.");
    }
}