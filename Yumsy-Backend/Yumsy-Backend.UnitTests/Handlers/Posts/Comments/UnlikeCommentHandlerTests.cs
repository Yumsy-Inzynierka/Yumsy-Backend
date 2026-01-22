using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Likes.UnlikeComment;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts.Comments;

public class UnlikeCommentHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new UnlikeCommentHandler(context);
        var request = new UnlikeCommentRequest
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

        var handler = new UnlikeCommentHandler(context);
        var commentId = Guid.NewGuid();
        var request = new UnlikeCommentRequest
        {
            CommentId = commentId,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Comment with ID: {commentId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_NotLiked()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var handler = new UnlikeCommentHandler(context);
        var request = new UnlikeCommentRequest
        {
            CommentId = comment.Id,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID: {user.Id} does not like comment with ID: {comment.Id}.");
    }

    [Fact]
    public async Task Handle_Should_UnlikeComment_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id);
        var commentLike = TestDataBuilder.CreateCommentLike(userId: user.Id, commentId: comment.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        context.CommentLikes.Add(commentLike);
        await context.SaveChangesAsync();

        var handler = new UnlikeCommentHandler(context);
        var request = new UnlikeCommentRequest
        {
            CommentId = comment.Id,
            UserId = user.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var likeExists = await context.CommentLikes
            .AnyAsync(cl => cl.CommentId == comment.Id && cl.UserId == user.Id);

        likeExists.Should().BeFalse();
    }
}
