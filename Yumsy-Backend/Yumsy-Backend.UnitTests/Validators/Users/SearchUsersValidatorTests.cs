using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.SearchUsers;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class SearchUsersValidatorTests
{
    private readonly SearchUsersValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_QueryIsEmpty()
    {
        var request = new SearchUsersRequest { Query = "", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Search query cannot be empty");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooShort()
    {
        var request = new SearchUsersRequest { Query = "a", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Search query must be at least 2 characters long");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooLong()
    {
        var request = new SearchUsersRequest { Query = new string('a', 51), Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Search query cannot exceed 50 characters");
    }

    [Fact]
    public void Should_HaveError_When_PageIsZero()
    {
        var request = new SearchUsersRequest { Query = "test", Page = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Page)
            .WithErrorMessage("Page must be greater than 0");
    }

    [Fact]
    public void Should_HaveError_When_PageIsNegative()
    {
        var request = new SearchUsersRequest { Query = "test", Page = -1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Page)
            .WithErrorMessage("Page must be greater than 0");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new SearchUsersRequest { Query = "test", Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("ab")]
    [InlineData("test query")]
    [InlineData("a very long but still valid search query under 50")]
    public void Should_NotHaveError_ForValidQueries(string query)
    {
        var request = new SearchUsersRequest { Query = query, Page = 1 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Query);
    }
}
