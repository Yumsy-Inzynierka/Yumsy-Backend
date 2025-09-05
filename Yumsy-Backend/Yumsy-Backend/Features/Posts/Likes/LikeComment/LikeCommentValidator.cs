using FluentValidation;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment
{
    public class LikeCommentValidator : AbstractValidator<LikeCommentRequest>
    {
        public LikeCommentValidator()
        {
            RuleFor(x => x.CommentId)
                .NotEmpty().WithMessage("CommentId is required.")
                .NotEqual(Guid.Empty).WithMessage("CommentId must be a valid GUID.");
            
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .NotEqual(Guid.Empty).WithMessage("UserId must be a valid GUID.");
        }
    }
}
