using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.EditShoppingList;

public class EditShoppingListValidator : AbstractValidator<EditShoppingListRequest>
{
    public EditShoppingListValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Ingredients)
            .NotEmpty();

        RuleForEach(x => x.Ingredients)
            .ChildRules(ingredient =>
            {
                ingredient.RuleFor(i => i.IngredientId).NotEmpty();
                ingredient.RuleFor(i => i.Quantity).GreaterThan(0);
            });
    }
}