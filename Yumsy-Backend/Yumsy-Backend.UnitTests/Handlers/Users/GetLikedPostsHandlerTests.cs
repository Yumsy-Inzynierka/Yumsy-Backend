using FluentAssertions;
using Yumsy_Backend.Features.Users.Profile.GetLikedPosts;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Users;

public class GetLikedPostsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetLikedPostsHandler(context);
        var userId = Guid.NewGuid();
        var request = new GetLikedPostsRequest
        {
            UserId = userId,
            CurrentPage = 1
        };

        var act = () => handler.Handle(request, userId, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"User with ID: {userId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoLikedPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new GetLikedPostsHandler(context);
        var request = new GetLikedPostsRequest
        {
            UserId = user.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result.Posts.Should().BeEmpty();
        result.HasMore.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnLikedPosts_When_UserHasLikes()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var like = TestDataBuilder.CreateLike(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Likes.Add(like);
        await context.SaveChangesAsync();

        var handler = new GetLikedPostsHandler(context);
        var request = new GetLikedPostsRequest
        {
            UserId = user.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.Posts.Should().HaveCount(1);
        result.Posts.First().Id.Should().Be(post.Id);
    }
}
