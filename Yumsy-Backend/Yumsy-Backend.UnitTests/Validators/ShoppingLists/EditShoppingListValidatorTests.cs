using FluentValidation.TestHelper;
using Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

namespace Yumsy_Backend.UnitTests.Validators.ShoppingLists;

public class EditShoppingListValidatorTests
{
    private readonly EditShoppingListValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.UserId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_HaveError_When_ShoppingListIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.ShoppingListId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.ShoppingListId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = null!
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body)
            .WithErrorMessage("Request body is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleIsEmpty()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = "",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 2 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleExceeds50Characters()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = new string('a', 51),
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 2 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title cannot exceed 50 characters.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientsIsEmpty()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated Shopping List",
                Ingredients = Enumerable.Empty<EditShoppingListIngredient>()
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Ingredients)
            .WithErrorMessage("Shopping list must contain at least one ingredient.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientIdIsEmpty()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated Shopping List",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.Empty, Quantity = 1 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].IngredientId");
    }

    [Fact]
    public void Should_HaveError_When_IngredientQuantityIsZero()
    {
        var request = new EditShoppingListRequest
        {
            UserId = Guid.NewGuid(),
            ShoppingListId = Guid.NewGuid(),
            Body = new EditShoppingListRequestBody
            {
                Title = "Updated Shopping List",
                Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 0 } }
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].Quantity")
            .WithErrorMessage("Quantity must be greater than 0.");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = CreateValidRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static EditShoppingListRequest CreateValidRequest() => new()
    {
        UserId = Guid.NewGuid(),
        ShoppingListId = Guid.NewGuid(),
        Body = new EditShoppingListRequestBody
        {
            Title = "Updated Shopping List",
            Ingredients = new[] { new EditShoppingListIngredient { IngredientId = Guid.NewGuid(), Quantity = 2 } }
        }
    };
}
