using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.Profile.GetLikedPosts;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class GetLikedPostsValidatorTests
{
    private readonly GetLikedPostsValidator _validator = new();

    [Fact]
    public void Should_NotHaveError_ForAnyRequest()
    {
        var request = new GetLikedPostsRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
