using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class RefreshTokenValidatorTests
{
    private readonly RefreshTokenValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_RefreshTokenIsEmpty()
    {
        var request = new RefreshTokenRequest { RefreshToken = "" };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.RefreshToken)
            .WithErrorMessage("Refresh token is required");
    }

    [Fact]
    public void Should_HaveError_When_RefreshTokenIsNull()
    {
        var request = new RefreshTokenRequest { RefreshToken = null! };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.RefreshToken)
            .WithErrorMessage("Refresh token is required");
    }

    [Fact]
    public void Should_NotHaveError_When_RefreshTokenIsProvided()
    {
        var request = new RefreshTokenRequest { RefreshToken = "valid-refresh-token-value" };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
