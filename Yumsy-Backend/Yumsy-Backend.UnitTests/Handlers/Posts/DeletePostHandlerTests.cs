using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.DeletePost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class DeletePostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new DeletePostHandler(context);
        var postId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var request = new DeletePostRequest
        {
            UserId = userId,
            PostId = postId
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {postId} for User ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserIsNotOwner()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var otherUser = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.AddRange(user, otherUser);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new DeletePostHandler(context);
        var request = new DeletePostRequest
        {
            UserId = otherUser.Id,
            PostId = post.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {post.Id} for User ID: {otherUser.Id} not found.");
    }

    [Fact]
    public async Task Handle_Should_DeletePost_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new DeletePostHandler(context);
        var request = new DeletePostRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var postExists = await context.Posts.AnyAsync(p => p.Id == post.Id);
        postExists.Should().BeFalse();
    }
}
