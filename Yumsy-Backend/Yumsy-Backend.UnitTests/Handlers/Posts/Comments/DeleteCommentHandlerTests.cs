using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Comments.DeleteComment;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts.Comments;

public class DeleteCommentHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_CommentDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new DeleteCommentHandler(context);
        var commentId = Guid.NewGuid();
        var request = new DeleteCommentRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.NewGuid(),
            CommentId = commentId
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Comment with ID: {commentId} not found or no permissions to delete it");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserIsNotOwner()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var otherUser = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id, content: "Test comment");

        context.Users.AddRange(user, otherUser);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var handler = new DeleteCommentHandler(context);
        var request = new DeleteCommentRequest
        {
            UserId = otherUser.Id,
            PostId = post.Id,
            CommentId = comment.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Comment with ID: {comment.Id} not found or no permissions to delete it");
    }

    [Fact]
    public async Task Handle_Should_DeleteComment_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id, content: "Test comment");

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var handler = new DeleteCommentHandler(context);
        var request = new DeleteCommentRequest
        {
            UserId = user.Id,
            PostId = post.Id,
            CommentId = comment.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var deletedComment = await context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);
        deletedComment.Should().BeNull();
    }
}
