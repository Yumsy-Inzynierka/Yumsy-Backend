using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.EditPost;

namespace Yumsy_Backend.UnitTests.Validators.Posts;

public class EditPostValidatorTests
{
    private readonly EditPostValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.PostId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = CreateValidRequest();
        request.UserId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_HaveError_When_BodyIsNull()
    {
        var request = new EditPostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Body = null!
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body)
            .WithErrorMessage("Request body is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Title = "";
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title is required.");
    }

    [Fact]
    public void Should_HaveError_When_TitleExceeds50Characters()
    {
        var request = CreateValidRequest();
        request.Body.Title = new string('a', 51);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Title)
            .WithErrorMessage("Title cannot exceed 50 characters.");
    }

    [Fact]
    public void Should_HaveError_When_DescriptionIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Description = "";
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Description)
            .WithErrorMessage("Description is required.");
    }

    [Fact]
    public void Should_HaveError_When_DescriptionExceeds400Characters()
    {
        var request = CreateValidRequest();
        request.Body.Description = new string('a', 401);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Description)
            .WithErrorMessage("Description cannot exceed 400 characters.");
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = CreateValidRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static EditPostRequest CreateValidRequest() => new()
    {
        PostId = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        Body = new EditPostRequestBody
        {
            Title = "Updated Recipe Title",
            Description = "Updated recipe description"
        }
    };
}
