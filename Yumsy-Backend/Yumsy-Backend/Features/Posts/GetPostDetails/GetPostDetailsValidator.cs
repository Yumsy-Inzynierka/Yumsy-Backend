using FluentValidation;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsValidator : AbstractValidator<GetPostDetailsRequest>
{
    public GetPostDetailsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
        
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.")
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}