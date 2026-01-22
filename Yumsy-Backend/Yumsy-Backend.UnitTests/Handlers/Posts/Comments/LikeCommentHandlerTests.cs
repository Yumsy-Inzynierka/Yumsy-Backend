using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Likes.LikeComment;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts.Comments;

public class LikeCommentHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new LikeCommentHandler(context);
        var request = new LikeCommentRequest
        {
            CommentId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {request.UserId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_CommentDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new LikeCommentHandler(context);
        var commentId = Guid.NewGuid();
        var request = new LikeCommentRequest
        {
            CommentId = commentId,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Comment with ID: {commentId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_AlreadyLiked()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id);
        var existingLike = TestDataBuilder.CreateCommentLike(userId: user.Id, commentId: comment.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        context.CommentLikes.Add(existingLike);
        await context.SaveChangesAsync();

        var handler = new LikeCommentHandler(context);
        var request = new LikeCommentRequest
        {
            CommentId = comment.Id,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID: {user.Id} already liked comment with ID: {comment.Id}.");
    }

    [Fact]
    public async Task Handle_Should_LikeComment_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var handler = new LikeCommentHandler(context);
        var request = new LikeCommentRequest
        {
            CommentId = comment.Id,
            UserId = user.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var commentLike = await context.CommentLikes
            .FirstOrDefaultAsync(cl => cl.CommentId == comment.Id && cl.UserId == user.Id);

        commentLike.Should().NotBeNull();
        commentLike!.CommentId.Should().Be(comment.Id);
        commentLike.UserId.Should().Be(user.Id);
    }
}
