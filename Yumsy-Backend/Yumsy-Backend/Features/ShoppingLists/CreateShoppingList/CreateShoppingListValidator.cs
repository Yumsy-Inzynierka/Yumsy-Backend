using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListValidator : AbstractValidator<CreateShoppingListRequest>
{
    public CreateShoppingListValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new CreateShoppingListRequestBodyValidator());
    }
}

public class CreateShoppingListRequestBodyValidator : AbstractValidator<CreateShoppingListRequestBody>
{
    public CreateShoppingListRequestBodyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(x => x.Ingredients)
            .NotNull().WithMessage("Ingredients are required.")
            .Must(ingredients => ingredients.Any())
            .WithMessage("Shopping list must contain at least one ingredient.");

        RuleForEach(x => x.Ingredients).SetValidator(new AddShoppingListIngredientValidator());
    }
}

public class AddShoppingListIngredientValidator : AbstractValidator<AddShoppingListIngredientRequest>
{
    public AddShoppingListIngredientValidator()
    {
        RuleFor(i => i.Id)
            .NotEmpty().WithMessage("Ingredient Id is required.")
            .NotEqual(Guid.Empty).WithMessage("Ingredient Id must be a valid GUID.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Ingredient quantity must be greater than 0.");
    }
}