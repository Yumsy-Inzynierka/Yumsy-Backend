using FluentAssertions;
using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.Login;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class LoginValidatorTests
{
    private readonly LoginValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_EmailIsEmpty()
    {
        var request = new LoginRequest { Email = "", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is mandatory");
    }

    [Fact]
    public void Should_HaveError_When_EmailIsInvalidFormat()
    {
        var request = new LoginRequest { Email = "invalid-email", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email address is not valid");
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsEmpty()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password is mandatory");
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsTooShort()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "Short1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must be at least 8 characters long");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksUppercase()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "password1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one uppercase letter");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksLowercase()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "PASSWORD1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one lowercase letter");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksNumber()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one number");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksSpecialCharacter()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "Password1" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one special character");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new LoginRequest { Email = "test@example.com", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("user@domain.com")]
    [InlineData("user.name@domain.co.uk")]
    [InlineData("user+tag@example.org")]
    public void Should_NotHaveError_ForValidEmails(string email)
    {
        var request = new LoginRequest { Email = email, Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }

    [Theory]
    [InlineData("Password1!")]
    [InlineData("MyP@ssw0rd")]
    [InlineData("Str0ng#Pass")]
    public void Should_NotHaveError_ForValidPasswords(string password)
    {
        var request = new LoginRequest { Email = "test@example.com", Password = password };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }
}
