using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class GetProfileDetailsValidatorTests
{
    private readonly GetProfileDetailsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetProfileDetailsRequest
        {
            UserId = Guid.Empty,
            ProfileOwnerId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new GetProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            ProfileOwnerId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
