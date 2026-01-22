using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Posts.Likes.UnlikePost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class UnlikePostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new UnlikePostHandler(context);
        var request = new UnlikePostRequest
        {
            PostId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {request.PostId} not found");
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidOperationException_When_UserHasNotLikedPost()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new UnlikePostHandler(context);
        var request = new UnlikePostRequest
        {
            PostId = post.Id,
            UserId = user.Id
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"User with ID: {user.Id} does not like post with ID: {post.Id}.");
    }

    [Fact]
    public async Task Handle_Should_RemoveLike_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var like = TestDataBuilder.CreateLike(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Likes.Add(like);
        await context.SaveChangesAsync();

        var handler = new UnlikePostHandler(context);
        var request = new UnlikePostRequest
        {
            PostId = post.Id,
            UserId = user.Id
        };

        await handler.Handle(request, CancellationToken.None);

        var likeExists = await context.Likes
            .AnyAsync(l => l.PostId == post.Id && l.UserId == user.Id);

        likeExists.Should().BeFalse();
    }
}
