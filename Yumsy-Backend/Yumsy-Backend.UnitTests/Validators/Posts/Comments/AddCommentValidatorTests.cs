using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Comments.AddComment;

namespace Yumsy_Backend.UnitTests.Validators.Posts.Comments;

public class AddCommentValidatorTests
{
    private readonly AddCommentValidator _validator = new();

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
        var request = new AddCommentRequest
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
    public void Should_HaveError_When_ContentIsEmpty()
    {
        var request = CreateValidRequest();
        request.Body.Content = "";
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Content)
            .WithErrorMessage("Content is required.");
    }

    [Fact]
    public void Should_HaveError_When_ContentExceeds400Characters()
    {
        var request = CreateValidRequest();
        request.Body.Content = new string('a', 401);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.Content)
            .WithErrorMessage("Content cannot exceed 400 characters.");
    }

    [Fact]
    public void Should_HaveError_When_ParentCommentIdIsEmptyGuid()
    {
        var request = CreateValidRequest();
        request.Body.ParentCommentId = Guid.Empty;
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Body.ParentCommentId)
            .WithErrorMessage("ParentCommentId must be a valid GUID or null.");
    }

    [Fact]
    public void Should_NotHaveError_When_ParentCommentIdIsNull()
    {
        var request = CreateValidRequest();
        request.Body.ParentCommentId = null;
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Body.ParentCommentId);
    }

    [Fact]
    public void Should_NotHaveError_When_ParentCommentIdIsValidGuid()
    {
        var request = CreateValidRequest();
        request.Body.ParentCommentId = Guid.NewGuid();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveValidationErrorFor(x => x.Body.ParentCommentId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = CreateValidRequest();
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }

    private static AddCommentRequest CreateValidRequest() => new()
    {
        PostId = Guid.NewGuid(),
        UserId = Guid.NewGuid(),
        Body = new AddCommentRequestBody
        {
            Content = "This is a test comment",
            ParentCommentId = null
        }
    };
}
