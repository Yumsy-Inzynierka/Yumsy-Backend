using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetPostsByTag;

public class GetPostsByTagValidator : AbstractValidator<GetPostsByTagRequest>
{
    public GetPostsByTagValidator()
    {
        RuleFor(x => x.TagId)
            .NotEmpty().WithMessage("Tag ID is required");
    }
}