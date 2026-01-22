using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.FollowUser;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class FollowUserValidatorTests
{
    private readonly FollowUserValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_FollowerIdIsEmpty()
    {
        var request = new FollowUserRequest
        {
            FollowerId = Guid.Empty,
            Body = new FollowUserRequestBody { FollowingId = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.FollowerId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new FollowUserRequest
        {
            FollowerId = Guid.NewGuid(),
            Body = null!
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body)
            .WithErrorMessage("Request body is required.");
    }

    [Fact]
    public void Should_HaveError_When_FollowingIdIsEmpty()
    {
        var request = new FollowUserRequest
        {
            FollowerId = Guid.NewGuid(),
            Body = new FollowUserRequestBody { FollowingId = Guid.Empty }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.FollowingId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new FollowUserRequest
        {
            FollowerId = Guid.NewGuid(),
            Body = new FollowUserRequestBody { FollowingId = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
