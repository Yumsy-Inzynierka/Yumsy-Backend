using FluentValidation;

namespace Yumsy_Backend.Features.Posts.AddPost;

public class AddPostValidator : AbstractValidator<AddPostRequest>
{
    public AddPostValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new AddPostRequestBodyValidator());
    }
}

public class AddPostRequestBodyValidator : AbstractValidator<AddPostRequestBody>
{
    public AddPostRequestBodyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(400).WithMessage("Description cannot exceed 400 characters.");

        RuleFor(x => x.CookingTime)
            .GreaterThan(0).WithMessage("CookingTime must be greater than 0.")
            .When(x => x.CookingTime.HasValue);

        RuleFor(x => x.Tags)
            .NotNull().WithMessage("Tags are required.")
            .Must(tags => tags.Any()).WithMessage("At least one tag is required.");

        RuleFor(x => x.Images)
            .NotNull().WithMessage("At least one image is required.")
            .Must(images => images.Any()).WithMessage("At least one image is required.");

        RuleFor(x => x.Ingredients)
            .NotNull().WithMessage("Ingredients are required.")
            .Must(ingredients => ingredients.Any()).WithMessage("At least one ingredient is required.");

        RuleFor(x => x.RecipeSteps)
            .NotNull().WithMessage("Recipe steps are required.")
            .Must(steps => steps.Any()).WithMessage("At least one recipe step is required.")
            .Must(HaveSequentialSteps).WithMessage("Recipe steps must start from 1 and be sequential without duplicates.");

        RuleForEach(x => x.Tags).SetValidator(new AddPostTagValidator());
        RuleForEach(x => x.Images).SetValidator(new AddPostImageValidator());
        RuleForEach(x => x.Ingredients).SetValidator(new AddPostIngredientValidator());
        RuleForEach(x => x.RecipeSteps).SetValidator(new AddPostRecipeStepValidator());
    }

    private bool HaveSequentialSteps(IEnumerable<AddPostRequestRecipeStep> steps)
    {
        var ordered = steps.Select(s => s.StepNumber).OrderBy(n => n).ToList();
        if (!ordered.Any()) return false;
        if (ordered.First() != 1) return false;
        return ordered.SequenceEqual(Enumerable.Range(1, ordered.Count));
    }
}

public class AddPostTagValidator : AbstractValidator<AddPostRequestTag>
{
    public AddPostTagValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Tag Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Tag Id must be a valid GUID.");
    }
}

public class AddPostImageValidator : AbstractValidator<AddPostRequestImage>
{
    public AddPostImageValidator()
    {
        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required.")
            .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Image must be a valid URL.");
    }
}

public class AddPostIngredientValidator : AbstractValidator<AddPostRequestIngredient>
{
    public AddPostIngredientValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Ingredient Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Ingredient Id must be a valid GUID.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Ingredient quantity must be greater than 0.");
    }
}

public class AddPostRecipeStepValidator : AbstractValidator<AddPostRequestRecipeStep>
{
    public AddPostRecipeStepValidator()
    {
        RuleFor(x => x.StepNumber)
            .GreaterThan(0).WithMessage("StepNumber must be greater than 0.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Step description is required.")
            .MaximumLength(300).WithMessage("Step description cannot exceed 300 characters.");

        RuleFor(x => x.Image)
            .Must(uri => uri == null || Uri.IsWellFormedUriString(uri, UriKind.Absolute))
            .WithMessage("Step image must be a valid URL if provided.");
    }
}
