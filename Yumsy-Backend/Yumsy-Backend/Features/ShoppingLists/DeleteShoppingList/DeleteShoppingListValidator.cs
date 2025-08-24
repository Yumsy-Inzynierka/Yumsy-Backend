using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;

public class DeleteShoppingListValidator : AbstractValidator<DeleteShoppingListRequest>
{
    public DeleteShoppingListValidator()
    {
        RuleFor(x => x.ShoppingListId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}