using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.GetPostDetails;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class GetPostDetailsValidatorTests
{
    private readonly GetPostDetailsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetPostDetailsRequest
        {
            UserId = Guid.Empty,
            PostId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new GetPostDetailsRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.Empty
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new GetPostDetailsRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
