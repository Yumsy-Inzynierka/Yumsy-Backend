using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.GetSavedPosts;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class GetSavedPostsValidatorTests
{
    private readonly GetSavedPostsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_CurrentPageIsZero()
    {
        var request = new GetSavedPostsRequest { CurrentPage = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.CurrentPage)
            .WithErrorMessage("Page must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_When_CurrentPageIsNegative()
    {
        var request = new GetSavedPostsRequest { CurrentPage = -1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.CurrentPage)
            .WithErrorMessage("Page must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_When_CurrentPageIsValid()
    {
        var request = new GetSavedPostsRequest { CurrentPage = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Should_NotHaveError_ForValidPageNumbers(int page)
    {
        var request = new GetSavedPostsRequest { CurrentPage = page };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.CurrentPage);
    }
}
