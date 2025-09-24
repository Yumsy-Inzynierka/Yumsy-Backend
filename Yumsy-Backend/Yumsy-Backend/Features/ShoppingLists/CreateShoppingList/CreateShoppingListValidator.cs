using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;

public class CreateShoppingListValidator : AbstractValidator<CreateShoppingListRequest>
{
    public CreateShoppingListValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(50).WithMessage("Title cannot exceed 50 characters");
        
        RuleFor(x => x.Ingredients)
            .NotEmpty().WithMessage("Shopping list must contain at least one ingredient");

        RuleForEach(x => x.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(i => i.Id).NotEmpty();
                ingredient.RuleFor(i => i.Quantity).GreaterThan(0);
            });
    }
}