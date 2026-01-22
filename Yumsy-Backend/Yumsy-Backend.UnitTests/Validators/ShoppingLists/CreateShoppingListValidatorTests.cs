using FluentValidation.TestHelper;
using Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

namespace Yumsy_Backend.UnitTests.Validators.ShoppingLists;

public class CreateShoppingListValidatorTests
{
    private readonly CreateShoppingListValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.UserId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = null!
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body)
            .WithErrorMessage("Request body is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleIsEmpty()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = "",
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.NewGuid(), Quantity = 2 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleExceeds50Characters()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = new string('a', 51),
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.NewGuid(), Quantity = 2 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title cannot exceed 50 characters.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientsIsEmpty()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = "Test Shopping List",
                Ingredients = Enumerable.Empty<AddShoppingListIngredientRequest>()
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Ingredients)
            .WithErrorMessage("Shopping list must contain at least one ingredient.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientIdIsEmpty()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = "Test Shopping List",
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.Empty, Quantity = 1 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].Id");
    }

    [Fact]
    public void Should_HaveError_When_IngredientQuantityIsZero()
    {
        var request = new CreateShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            Body = new CreateShoppingListRequestBody
            {
                Title = "Test Shopping List",
                Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.NewGuid(), Quantity = 0 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].Quantity")
            .WithErrorMessage("Ingredient quantity must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = CreateValidRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static CreateShoppingListRequest CreateValidRequest() => new()
    {
        UserId = Guid.NewGuid(),
        Body = new CreateShoppingListRequestBody
        {
            Title = "Test Shopping List",
            Ingredients = new[] { new AddShoppingListIngredientRequest { Id = Guid.NewGuid(), Quantity = 2 } }
        }
    };
}
