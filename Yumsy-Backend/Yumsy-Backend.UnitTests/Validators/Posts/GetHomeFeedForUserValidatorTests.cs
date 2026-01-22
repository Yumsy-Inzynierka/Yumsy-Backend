using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.GetHomeFeedForUser;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class GetHomeFeedForUserValidatorTests
{
    private readonly GetHomeFeedForUserValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new GetHomeFeedForUserRequest { UserId = Guid.Empty };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_UserIdIsValid()
    {
        var request = new GetHomeFeedForUserRequest { UserId = Guid.NewGuid() };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
