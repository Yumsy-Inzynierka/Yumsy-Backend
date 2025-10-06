using FluentValidation;

namespace Yumsy_Backend.Features.ShoppingLists.AddShoppingList;

public class AddShoppingListValidator : AbstractValidator<AddShoppingListRequest>
{
    public AddShoppingListValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}

public class AddShoppingListRequestBodyValidator : AbstractValidator<AddShoppingListRequestBody>
    {
        public AddShoppingListRequestBodyValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(50).WithMessage("Title cannot exceed 50 characters.");
            
            RuleFor(x => x.CreatedFrom)
                .NotEmpty().WithMessage("CreatedFrom is required")
                .NotEqual(Guid.Empty).WithMessage("CreatedFrom must be a valid GUID");
        }
    }