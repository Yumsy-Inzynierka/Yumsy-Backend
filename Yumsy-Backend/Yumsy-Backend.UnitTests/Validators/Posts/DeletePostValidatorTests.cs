using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.DeletePost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class DeletePostValidatorTests
{
    private readonly DeletePostValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new DeletePostRequest
        {
            PostId = Guid.Empty,
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_NotHaveError_When_PostIdIsValid()
    {
        var request = new DeletePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
