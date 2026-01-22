using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Ingredients.SearchIngredients;

namespace Yumsy_Backend.UnitTests.Validators.Ingredients;

public class SearchIngredientsValidatorTests
{
    private readonly SearchIngredientsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_QueryIsEmpty()
    {
        var request = new SearchIngredientsRequest { Query = "", Offset = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query cannot be empty");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooShort()
    {
        var request = new SearchIngredientsRequest { Query = "ab", Offset = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query has to be at least 3 characters long");
    }

    [Fact]
    public void Should_HaveError_When_QueryIsTooLong()
    {
        var request = new SearchIngredientsRequest { Query = new string('a', 51), Offset = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Query)
            .WithErrorMessage("Query has to be maximum 50 characters long");
    }

    [Fact]
    public void Should_HaveError_When_OffsetIsNegative()
    {
        var request = new SearchIngredientsRequest { Query = "sugar", Offset = -1 };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Offset)
            .WithErrorMessage("Offset cannot be negative");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new SearchIngredientsRequest { Query = "sugar", Offset = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("sugar")]
    [InlineData("a valid ingredient search query")]
    public void Should_NotHaveError_ForValidQueries(string query)
    {
        var request = new SearchIngredientsRequest { Query = query, Offset = 0 };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Query);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    public void Should_NotHaveError_ForValidOffsets(int offset)
    {
        var request = new SearchIngredientsRequest { Query = "sugar", Offset = offset };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Offset);
    }
}
