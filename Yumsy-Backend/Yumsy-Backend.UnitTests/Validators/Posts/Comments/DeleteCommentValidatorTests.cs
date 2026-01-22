using FluentValidation.TestHelper;
using Yumsy_Backend.Features.Posts.Comments.DeleteComment;

namespace Yumsy_Backend.UnitTests.Validators.Posts.Comments;

public class DeleteCommentValidatorTests
{
    private readonly DeleteCommentValidator _validator = new();

    [Fact]
    public void Should_HaveError_When_CommentIdIsEmpty()
    {
        var request = new DeleteCommentRequest
        {
            CommentId = Guid.Empty,
            PostId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.CommentId);
    }

    [Fact]
    public void Should_HaveError_When_PostIdIsEmpty()
    {
        var request = new DeleteCommentRequest
        {
            CommentId = Guid.NewGuid(),
            PostId = Guid.Empty
        };
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.PostId);
    }

    [Fact]
    public void Should_NotHaveError_When_RequestIsValid()
    {
        var request = new DeleteCommentRequest
        {
            CommentId = Guid.NewGuid(),
            PostId = Guid.NewGuid()
        };
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
