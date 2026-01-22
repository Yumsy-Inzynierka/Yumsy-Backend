using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Comments.AddComment;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts.Comments;

public class AddCommentHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new AddCommentHandler(context);
        var request = new AddCommentRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.NewGuid(),
            Body = new AddCommentRequestBody { Content = "Test comment" }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {request.UserId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new AddCommentHandler(context);
        var request = new AddCommentRequest
        {
            UserId = user.Id,
            PostId = Guid.NewGuid(),
            Body = new AddCommentRequestBody { Content = "Test comment" }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {request.PostId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_ParentCommentDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var parentCommentId = Guid.NewGuid();
        var handler = new AddCommentHandler(context);
        var request = new AddCommentRequest
        {
            UserId = user.Id,
            PostId = post.Id,
            Body = new AddCommentRequestBody
            {
                Content = "Test comment",
                ParentCommentId = parentCommentId
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Parent comment with ID: {parentCommentId} not found");
    }

    [Fact]
    public async Task Handle_Should_AddComment_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new AddCommentHandler(context);
        var request = new AddCommentRequest
        {
            UserId = user.Id,
            PostId = post.Id,
            Body = new AddCommentRequestBody { Content = "Test comment" }
        };

        var response = await handler.Handle(request, CancellationToken.None);

        response.Should().NotBeNull();
        response.Content.Should().Be("Test comment");
        response.PostId.Should().Be(post.Id);
        response.UserId.Should().Be(user.Id);

        var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == response.Id);
        comment.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_AddReply_When_ParentCommentExists()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var parentComment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id, content: "Parent comment");

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(parentComment);
        await context.SaveChangesAsync();

        var handler = new AddCommentHandler(context);
        var request = new AddCommentRequest
        {
            UserId = user.Id,
            PostId = post.Id,
            Body = new AddCommentRequestBody
            {
                Content = "Reply comment",
                ParentCommentId = parentComment.Id
            }
        };

        var response = await handler.Handle(request, CancellationToken.None);

        response.Should().NotBeNull();
        response.ParentCommentId.Should().Be(parentComment.Id);
    }
}
