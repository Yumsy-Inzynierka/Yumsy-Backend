using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Likes.LikePost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class LikePostValidatorTests
{
    private readonly LikePostValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new LikePostRequest
        {
            PostId = Guid.Empty,
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new LikePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
