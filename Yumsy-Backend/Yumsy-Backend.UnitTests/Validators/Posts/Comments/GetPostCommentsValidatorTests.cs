using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Comments.GetPostComments;

namespace Yumsy_Backend.UnitTests.Validators.Posts.Comments;

public class GetPostCommentsValidatorTests
{
    private readonly GetPostCommentsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new GetPostCommentsRequest { PostId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_NotHaveError_When_PostIdIsValid()
    {
        var request = new GetPostCommentsRequest { PostId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
