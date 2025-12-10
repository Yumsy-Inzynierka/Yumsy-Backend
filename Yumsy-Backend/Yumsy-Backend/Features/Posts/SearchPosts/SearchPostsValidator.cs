using FluentValidation;

namespace Yumsy_Backend.Features.Posts.SearchPosts;

public class SearchPostsValidator : AbstractValidator<SearchPostsRequest>
{
    public SearchPostsValidator()
    {
        RuleFor(x => x.Query)
            .NotEmpty()
            .WithMessage("Query cannot be empty")
            .MinimumLength(3)
            .WithMessage("Query has to be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Query has to be maximum 50 characters long");

        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page number must be greater than or equal to 1");
    }
}