using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Likes.LikePost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class LikePostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new LikePostHandler(context);
        var request = new LikePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {request.PostId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserAlreadyLikedPost()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var existingLike = TestDataBuilder.CreateLike(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Likes.Add(existingLike);
        await context.SaveChangesAsync();

        var handler = new LikePostHandler(context);
        var request = new LikePostRequest
        {
            PostId = post.Id,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {user.Id} have already liked post with ID: {post.Id}");
    }

    [Fact]
    public async Task Handle_Should_AddLike_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new LikePostHandler(context);
        var request = new LikePostRequest
        {
            PostId = post.Id,
            UserId = user.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var like = await context.Likes
            .FirstOrDefaultAsync(l => l.PostId == post.Id && l.UserId == user.Id);

        like.Should().NotBeNull();
        like!.PostId.Should().Be(post.Id);
        like.UserId.Should().Be(user.Id);
    }
}
