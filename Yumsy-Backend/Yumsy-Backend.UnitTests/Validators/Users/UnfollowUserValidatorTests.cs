using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.UnfollowUser;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class UnfollowUserValidatorTests
{
    private readonly UnfollowUserValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_FollowerIdIsEmpty()
    {
        var request = new UnfollowUserRequest
        {
            FollowerId = Guid.Empty,
            Body = new UnfollowUserRequestBody { FollowingId = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.FollowerId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new UnfollowUserRequest
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
        var request = new UnfollowUserRequest
        {
            FollowerId = Guid.NewGuid(),
            Body = new UnfollowUserRequestBody { FollowingId = Guid.Empty }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.FollowingId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new UnfollowUserRequest
        {
            FollowerId = Guid.NewGuid(),
            Body = new UnfollowUserRequestBody { FollowingId = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
