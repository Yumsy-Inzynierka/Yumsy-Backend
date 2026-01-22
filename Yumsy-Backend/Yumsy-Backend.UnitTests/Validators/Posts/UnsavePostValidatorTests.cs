using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.UnsavePost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class UnsavePostValidatorTests
{
    private readonly UnsavePostValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new UnsavePostRequest
        {
            PostId = Guid.Empty,
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new UnsavePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.Empty
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new UnsavePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
