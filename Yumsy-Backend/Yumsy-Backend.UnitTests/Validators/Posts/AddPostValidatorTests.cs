using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.AddPost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class AddPostValidatorTests
{
    private readonly AddPostValidator _validator = new();

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
        var request = new AddPostRequest
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
        var request = CreateValidRequest();
        request.Body.Title = "";
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleExceeds50Characters()
    {
        var request = CreateValidRequest();
        request.Body.Title = new string('a', 51);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title cannot exceed 50 characters.");
    }

    [Fact]
    public void Should_HaveError_When_DescriptionIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Description = "";
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Description)
            .WithErrorMessage("Description is required.");
    }

    [Fact]
    public void Should_HaveError_When_DescriptionExceeds400Characters()
    {
        var request = CreateValidRequest();
        request.Body.Description = new string('a', 401);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Description)
            .WithErrorMessage("Description cannot exceed 400 characters.");
    }

    [Fact]
    public void Should_HaveError_When_CookingTimeIsZeroOrNegative()
    {
        var request = CreateValidRequest();
        request.Body.CookingTime = 0;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.CookingTime)
            .WithErrorMessage("CookingTime must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_When_TagsIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Tags = Enumerable.Empty<AddPostRequestTag>();
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Tags)
            .WithErrorMessage("At least one tag is required.");
    }

    [Fact]
    public void Should_HaveError_When_TagIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Tags = new[] { new AddPostRequestTag { Id = Guid.Empty } };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Tags[0].Id");
    }

    [Fact]
    public void Should_HaveError_When_ImagesIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Images = Enumerable.Empty<AddPostRequestImage>();
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Images)
            .WithErrorMessage("At least one image is required.");
    }

    [Fact]
    public void Should_HaveError_When_ImageUrlIsInvalid()
    {
        var request = CreateValidRequest();
        request.Body.Images = new[] { new AddPostRequestImage { Image = "not-a-url" } };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Images[0].Image")
            .WithErrorMessage("Image must be a valid URL.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientsIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Ingredients = Enumerable.Empty<AddPostRequestIngredient>();
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Ingredients)
            .WithErrorMessage("At least one ingredient is required.");
    }

    [Fact]
    public void Should_HaveError_When_IngredientIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Ingredients = new[] { new AddPostRequestIngredient { Id = Guid.Empty, Quantity = 100 } };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].Id");
    }

    [Fact]
    public void Should_HaveError_When_IngredientQuantityIsZero()
    {
        var request = CreateValidRequest();
        request.Body.Ingredients = new[] { new AddPostRequestIngredient { Id = Guid.NewGuid(), Quantity = 0 } };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.Ingredients[0].Quantity")
            .WithErrorMessage("Ingredient quantity must be greater than 0.");
    }

    [Fact]
    public void Should_HaveError_When_RecipeStepsIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.RecipeSteps = Enumerable.Empty<AddPostRequestRecipeStep>();
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.RecipeSteps)
            .WithErrorMessage("At least one recipe step is required.");
    }

    [Fact]
    public void Should_HaveError_When_RecipeStepsAreNotSequential()
    {
        var request = CreateValidRequest();
        request.Body.RecipeSteps = new[]
        {
            new AddPostRequestRecipeStep { StepNumber = 1, Description = "Step 1" },
            new AddPostRequestRecipeStep { StepNumber = 3, Description = "Step 3" }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.RecipeSteps)
            .WithErrorMessage("Recipe steps must start from 1 and be sequential without duplicates.");
    }

    [Fact]
    public void Should_HaveError_When_RecipeStepDescriptionIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.RecipeSteps = new[]
        {
            new AddPostRequestRecipeStep { StepNumber = 1, Description = "" }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.RecipeSteps[0].Description")
            .WithErrorMessage("Step description is required.");
    }

    [Fact]
    public void Should_HaveError_When_StepDescriptionExceeds300Characters()
    {
        var request = CreateValidRequest();
        request.Body.RecipeSteps = new[]
        {
            new AddPostRequestRecipeStep { StepNumber = 1, Description = new string('a', 301) }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor("Body.RecipeSteps[0].Description")
            .WithErrorMessage("Step description cannot exceed 300 characters.");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = CreateValidRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_NotHaveError_When_CookingTimeIsNull()
    {
        var request = CreateValidRequest();
        request.Body.CookingTime = null;
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Body.CookingTime);
    }

    private static AddPostRequest CreateValidRequest() => new()
    {
        UserId = Guid.NewGuid(),
        Body = new AddPostRequestBody
        {
            Title = "Test Recipe",
            Description = "A delicious test recipe",
            CookingTime = 30,
            Tags = new[] { new AddPostRequestTag { Id = Guid.NewGuid() } },
            Images = new[] { new AddPostRequestImage { Image = "https://example.com/image.jpg" } },
            Ingredients = new[] { new AddPostRequestIngredient { Id = Guid.NewGuid(), Quantity = 100 } },
            RecipeSteps = new[] { new AddPostRequestRecipeStep { StepNumber = 1, Description = "Mix ingredients" } }
        }
    };
}
