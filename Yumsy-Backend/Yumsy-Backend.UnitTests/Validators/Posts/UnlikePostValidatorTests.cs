using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Likes.UnlikePost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class UnlikePostValidatorTests
{
    private readonly UnlikePostValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new UnlikePostRequest
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
        var request = new UnlikePostRequest
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
        var request = new UnlikePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
