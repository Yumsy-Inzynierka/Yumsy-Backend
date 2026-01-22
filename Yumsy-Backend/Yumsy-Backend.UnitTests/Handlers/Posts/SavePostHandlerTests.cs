using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.SavePost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class SavePostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new SavePostHandler(context);
        var request = new SavePostRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.NewGuid()
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

        var handler = new SavePostHandler(context);
        var request = new SavePostRequest
        {
            UserId = user.Id,
            PostId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {request.PostId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_PostAlreadySaved()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var saved = TestDataBuilder.CreateSaved(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Saved.Add(saved);
        await context.SaveChangesAsync();

        var handler = new SavePostHandler(context);
        var request = new SavePostRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Post is already saved by this user");
    }

    [Fact]
    public async Task Handle_Should_SavePost_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new SavePostHandler(context);
        var request = new SavePostRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var savedPost = await context.Saved
            .FirstOrDefaultAsync(s => s.PostId == post.Id && s.UserId == user.Id);

        savedPost.Should().NotBeNull();
        savedPost!.PostId.Should().Be(post.Id);
        savedPost.UserId.Should().Be(user.Id);
    }
}
