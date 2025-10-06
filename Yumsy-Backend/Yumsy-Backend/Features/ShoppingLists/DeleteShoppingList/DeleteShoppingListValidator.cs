using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

public class DeleteShoppingListValidator : AbstractValidator<DeleteShoppingListRequest>
{
    public DeleteShoppingListValidator()
    {
        RuleFor(x => x.ShoppingListId)
            .NotEmpty().WithMessage("ShoppingListId is required.")
            .NotEqual(Guid.Empty).WithMessage("ShoppingListId must be a valid GUID.");
    }
}