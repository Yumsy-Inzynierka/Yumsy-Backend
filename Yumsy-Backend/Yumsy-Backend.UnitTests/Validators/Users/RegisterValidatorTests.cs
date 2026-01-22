using FluentAssertions;
using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.Register;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class RegisterValidatorTests
{
    private readonly RegisterValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_EmailIsEmpty()
    {
        var request = new RegisterRequest { Email = "", Username = "testuser", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email is mandatory");
    }

    [Fact]
    public void Should_HaveError_When_EmailIsInvalidFormat()
    {
        var request = new RegisterRequest { Email = "invalid-email", Username = "testuser", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Email)
            .WithErrorMessage("Email address is not valid");
    }

    [Fact]
    public void Should_HaveError_When_UsernameIsEmpty()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username is mandatory");
    }

    [Fact]
    public void Should_HaveError_When_UsernameIsTooShort()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "abc", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username must be at least 3 characters long");
    }

    [Fact]
    public void Should_HaveError_When_UsernameIsTooLong()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "a".PadRight(21, 'a'), Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username must not exceed 20 characters");
    }

    [Fact]
    public void Should_HaveError_When_UsernameHasInvalidCharacters()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "user@name!", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Username)
            .WithErrorMessage("Username can only contain letters, digits, underscores, hyphens, and periods");
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsEmpty()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password is mandatory");
    }

    [Fact]
    public void Should_HaveError_When_PasswordIsTooShort()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "Short1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must be at least 8 characters long");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksUppercase()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "password1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one uppercase letter");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksLowercase()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "PASSWORD1!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one lowercase letter");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksNumber()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one number");
    }

    [Fact]
    public void Should_HaveError_When_PasswordLacksSpecialCharacter()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "Password1" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Password)
            .WithErrorMessage("Password must contain at least one special character");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("user123")]
    [InlineData("user_name")]
    [InlineData("user-name")]
    [InlineData("user.name")]
    [InlineData("User_Name-123")]
    public void Should_NotHaveError_ForValidUsernames(string username)
    {
        var request = new RegisterRequest { Email = "test@example.com", Username = username, Password = "Valid1Password!" };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
    }
}
