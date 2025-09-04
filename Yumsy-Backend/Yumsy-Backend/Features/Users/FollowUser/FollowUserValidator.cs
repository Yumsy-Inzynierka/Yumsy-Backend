using FluentValidation;

namespace Yumsy_Backend.Features.Users.FollowUser
{
    public class FollowUserValidator : AbstractValidator<FollowUserRequest>
    {
        public FollowUserValidator()
        {
            
            RuleFor(x => x.FollowerId)
                .NotEmpty().WithMessage("FollowerId is required.")
                .NotEqual(Guid.Empty).WithMessage("FollowerId must be a valid GUID.");
            
            RuleFor(x => x.FollowingId)
                .NotEmpty().WithMessage("FollowingId is required.")
                .NotEqual(Guid.Empty).WithMessage("FollowingId must be a valid GUID.");
        }
    }
}
