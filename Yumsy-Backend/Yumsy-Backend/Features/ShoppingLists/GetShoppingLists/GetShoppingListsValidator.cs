using FluentValidation;

namespace Yumsy_Backend.Features.Users.GetShoppingLists;

public class GetShoppingListsValidator : AbstractValidator<GetShoppingListsRequest>
{
    public GetShoppingListsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}