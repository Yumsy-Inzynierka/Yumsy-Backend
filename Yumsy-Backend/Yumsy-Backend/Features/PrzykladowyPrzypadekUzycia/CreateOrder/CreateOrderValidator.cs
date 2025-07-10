namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.CreateOrder;

using FluentValidation;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
    public CreateOrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.ProductIds).NotEmpty().WithMessage("At least one product must be selected.");
    }
}
