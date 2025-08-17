using FluentValidation;
using Yumsy_Backend.Features.Posts.GetHomeFeed;

namespace Yumsy_Backend.Features.Ingredients.SearchIngredient;

public class SearchIngredientsValidator : AbstractValidator<SearchIngredientsRequest>
{
    public SearchIngredientsValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Query cannot be empty")
            .MinimumLength(3)
            .WithMessage("Query has to be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Query has to be maximum 50 characters long");

        RuleFor(x => x.Offset)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Offset cannot be negative");
    }
    
}