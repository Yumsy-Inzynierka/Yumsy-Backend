using FluentValidation.TestHelper;
using Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

namespace Yumsy_Backend.UnitTests.Validators.ShoppingLists;

public class AddShoppingListValidatorTests
{
    private readonly AddShoppingListValidator _validator = new();
    private readonly AddShoppingListRequestBodyValidator _bodyValidator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new AddShoppingListRequest
        {
            UserId = Guid.Empty,
            Body = new AddShoppingListRequestBody { Title = "Test", CreatedFrom = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new AddShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new AddShoppingListRequestBody { Title = "Test", CreatedFrom = Guid.NewGuid() }
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Body_Should_HaveError_When_TitleIsEmpty()
    {
        var body = new AddShoppingListRequestBody { Title = "", CreatedFrom = Guid.NewGuid() };
        var result = _bodyValidator.TestValidate(body);
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Body_Should_HaveError_When_TitleExceeds50Characters()
    {
        var body = new AddShoppingListRequestBody { Title = new string('a', 51), CreatedFrom = Guid.NewGuid() };
        var result = _bodyValidator.TestValidate(body);
        result.ShouldHaveValidationErrorFor(x => x.Title)
            .WithErrorMessage("Title cannot exceed 50 characters.");
    }

    [Fact]
    public void Body_Should_HaveError_When_CreatedFromIsEmpty()
    {
        var body = new AddShoppingListRequestBody { Title = "Test", CreatedFrom = Guid.Empty };
        var result = _bodyValidator.TestValidate(body);
        result.ShouldHaveValidationErrorFor(x => x.CreatedFrom);
    }

    [Fact]
    public void Body_Should_NotHaveError_When_BodyIsValid()
    {
        var body = new AddShoppingListRequestBody { Title = "Test Shopping List", CreatedFrom = Guid.NewGuid() };
        var result = _bodyValidator.TestValidate(body);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
