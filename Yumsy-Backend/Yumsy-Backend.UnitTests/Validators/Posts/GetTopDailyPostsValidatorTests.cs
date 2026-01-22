using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.GetTopDailyPosts;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class GetTopDailyPostsValidatorTests
{
    private readonly GetTopDailyPostsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetTopDailyPostsRequest { UserId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new GetTopDailyPostsRequest { UserId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
