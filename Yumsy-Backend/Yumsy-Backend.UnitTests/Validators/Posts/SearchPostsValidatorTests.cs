using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.SearchPosts;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class SearchPostsValidatorTests
{
    private readonly SearchPostsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_QueryIsEmpty()
    {
        var request = new SearchPostsRequest { Query = "", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query cannot be empty");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooShort()
    {
        var request = new SearchPostsRequest { Query = "ab", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query has to be at least 3 characters long");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooLong()
    {
        var request = new SearchPostsRequest { Query = new string('a', 51), Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query has to be maximum 50 characters long");
    }

    [Fact]
    public void Should_HaveError_When_PageIsZero()
    {
        var request = new SearchPostsRequest { Query = "test", Page = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Page)
            .WithErrorMessage("Page number must be greater than or equal to 1");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new SearchPostsRequest { Query = "recipe", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("pasta recipe")]
    [InlineData("a valid search query that is under 50 characters")]
    public void Should_NotHaveError_ForValidQueries(string query)
    {
        var request = new SearchPostsRequest { Query = query, Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Query);
    }
}
