using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

namespace Yumsy_Backend.UnitTests.Validators.Users;

public class EditProfileDetailsValidatorTests
{
    private readonly EditProfileDetailsValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.Empty,
            Body = CreateValidBody()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = null!
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body)
            .WithErrorMessage("Request body is required.");
    }

    [Fact]
    public void Should_HaveError_When_ProfileNameExceeds20Characters()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = new EditProfileDetailsRequestBody
            {
                ProfileName = new string('a', 21),
                Bio = "Valid bio",
                ProfilePicture = "https://example.com/image.jpg"
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.ProfileName)
            .WithErrorMessage("ProfileName cannot exceed 20 characters.");
    }

    [Fact]
    public void Should_HaveError_When_BioExceeds400Characters()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = new EditProfileDetailsRequestBody
            {
                ProfileName = "Valid Name",
                Bio = new string('a', 401),
                ProfilePicture = "https://example.com/image.jpg"
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Bio)
            .WithErrorMessage("Bio cannot exceed 400 characters.");
    }

    [Fact]
    public void Should_HaveError_When_ProfilePictureIsInvalidUrl()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = new EditProfileDetailsRequestBody
            {
                ProfileName = "Valid Name",
                Bio = "Valid bio",
                ProfilePicture = "not-a-valid-url"
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.ProfilePicture)
            .WithErrorMessage("ProfilePicture must be a valid URL.");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = CreateValidBody()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("https://example.com/image.jpg")]
    [InlineData("http://cdn.site.com/pic.png")]
    [InlineData("https://storage.googleapis.com/bucket/image.webp")]
    public void Should_NotHaveError_ForValidProfilePictureUrls(string url)
    {
        var request = new EditProfileDetailsRequest
        {
            UserId = Guid.NewGuid(),
            Body = new EditProfileDetailsRequestBody
            {
                ProfileName = "Test",
                Bio = "Test bio",
                ProfilePicture = url
            }
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Body.ProfilePicture);
    }

    private static EditProfileDetailsRequestBody CreateValidBody() => new()
    {
        ProfileName = "Test User",
        Bio = "This is a test bio",
        ProfilePicture = "https://example.com/image.jpg"
    };
}
