using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public class GetPostCommentsValidator : AbstractValidator<GetPostCommentsRequest>
{
    public GetPostCommentsValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("PostId is required.") 
            .NotEqual(Guid.Empty).WithMessage("PostId must be a valid GUID.");
    }
}