using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public class EditShoppingListValidator : AbstractValidator<EditShoppingListRequest>
{
    public EditShoppingListValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(x => x.ShoppingListId)
            .NotEmpty().WithMessage("ShoppingListId is required.")
            .NotEqual(Guid.Empty).WithMessage("ShoppingListId must be a valid GUID.");

        RuleFor(x => x.Body)
            .NotNull().WithMessage("Request body is required.")
            .SetValidator(new EditShoppingListRequestBodyValidator());
    }
}

public class EditShoppingListRequestBodyValidator : AbstractValidator<EditShoppingListRequestBody>
{
    public EditShoppingListRequestBodyValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");

        RuleFor(x => x.Ingredients)
            .NotNull().WithMessage("Ingredients are required.")
            .Must(i => i.Any()).WithMessage("Shopping list must contain at least one ingredient.");

        RuleForEach(x => x.Ingredients).SetValidator(new EditShoppingListIngredientValidator());
    }
}

public class EditShoppingListIngredientValidator : AbstractValidator<EditShoppingListIngredient>
{
    public EditShoppingListIngredientValidator()
    {
        RuleFor(i => i.IngredientId)
            .NotEmpty().WithMessage("IngredientId is required.")
            .NotEqual(Guid.Empty).WithMessage("IngredientId must be a valid GUID.");

        RuleFor(i => i.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}