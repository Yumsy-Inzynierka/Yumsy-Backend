using FluentValidation;

namespace Yumsy_Backend.Features.Users.UnfollowUser
{
    public class UnfollowUserValidator : AbstractValidator<UnfollowUserRequest>
    {
        public UnfollowUserValidator()
        {
            RuleFor(x => x.FollowerId)
                .NotEmpty().WithMessage("FollowerId is required.")
                .NotEqual(Guid.Empty).WithMessage("FollowerId must be a valid GUID.");

            RuleFor(x => x.Body)
                .NotNull().WithMessage("Request body is required.")
                .SetValidator(new UnfollowUserRequestBodyValidator());
        }
    }

    public class UnfollowUserRequestBodyValidator : AbstractValidator<UnfollowUserRequestBody>
    {
        public UnfollowUserRequestBodyValidator()
        {
            RuleFor(x => x.FollowingId)
                .NotEmpty().WithMessage("FollowingId is required.")
                .NotEqual(Guid.Empty).WithMessage("FollowingId must be a valid GUID.");
        }
    }
}