using FluentValidation;

namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.Szablon;

public class Validator : AbstractValidator<Request>
{
    public Validator()
    {
        RuleFor(x => x);
    }
}