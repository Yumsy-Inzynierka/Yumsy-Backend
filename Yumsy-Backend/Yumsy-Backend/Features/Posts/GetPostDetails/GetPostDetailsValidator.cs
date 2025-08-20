using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostValidator : AbstractValidator<GetPostDetailsRequest>
{
    public GetPostValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}