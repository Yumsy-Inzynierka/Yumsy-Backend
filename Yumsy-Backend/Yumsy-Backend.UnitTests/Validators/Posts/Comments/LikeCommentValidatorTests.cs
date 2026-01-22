using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Likes.LikeComment;

namespace Yumsy_Backend.UnitTests.Validators.Posts.Comments;

public class LikeCommentValidatorTests
{
    private readonly LikeCommentValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_CommentIdIsEmpty()
    {
        var request = new LikeCommentRequest
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
        var request = new LikeCommentRequest
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
        var request = new LikeCommentRequest
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
