using FluentValidation;

namespace Yumsy_Backend.Features.Users.SearchUsers;

public class SearchUsersValidator : AbstractValidator<SearchUsersRequest>
{
    public SearchUsersValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Search query cannot be empty")
            .MinimumLength(2)
            .WithMessage("Search query must be at least 2 characters long")
            .MaximumLength(50)
            .WithMessage("Search query cannot exceed 50 characters");

        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("Page must be greater than 0");
    }
}