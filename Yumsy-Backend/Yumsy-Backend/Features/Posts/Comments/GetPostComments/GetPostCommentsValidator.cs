using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Comments.GetPostComments;

public class GetPostCommentsValidator : AbstractValidator<GetPostCommentsRequest>
{
    public GetPostCommentsValidator()
    {
        RuleFor(r => r.PostId)
            .NotEmpty().WithMessage("PostId is required");
    }
}