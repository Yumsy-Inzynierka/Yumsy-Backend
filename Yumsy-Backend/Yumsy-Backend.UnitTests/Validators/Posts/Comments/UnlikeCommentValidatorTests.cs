using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

namespace Yumsy_Backend.UnitTests.Validators.Posts.Comments;

public class UnlikeCommentValidatorTests
{
    private readonly UnlikeCommentValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_CommentIdIsEmpty()
    {
        var request = new UnlikeCommentRequest
        {
            CommentId = Guid.Empty,
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.CommentId);
    }

    [Fact]
    public void Should_HaveError_When_UserIdIsEmpty()
    {
        var request = new UnlikeCommentRequest
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.Empty
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new UnlikeCommentRequest
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
