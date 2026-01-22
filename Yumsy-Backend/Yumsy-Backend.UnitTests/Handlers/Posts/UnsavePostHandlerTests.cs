using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.UnsavePost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class UnsavePostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new UnsavePostHandler(context);
        var request = new UnsavePostRequest
        {
            UserId = Guid.NewGuid(),
            PostId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {request.PostId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_PostNotSaved()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new UnsavePostHandler(context);
        var request = new UnsavePostRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Post is not saved by this user");
    }

    [Fact]
    public async Task Handle_Should_UnsavePost_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var saved = TestDataBuilder.CreateSaved(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Saved.Add(saved);
        await context.SaveChangesAsync();

        var handler = new UnsavePostHandler(context);
        var request = new UnsavePostRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var savedExists = await context.Saved
            .AnyAsync(s => s.PostId == post.Id && s.UserId == user.Id);

        savedExists.Should().BeFalse();
    }
}
