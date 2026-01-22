using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.GetNewPosts;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class GetNewPostsValidatorTests
{
    private readonly GetNewPostsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetNewPostsRequest { UserId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new GetNewPostsRequest { UserId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
