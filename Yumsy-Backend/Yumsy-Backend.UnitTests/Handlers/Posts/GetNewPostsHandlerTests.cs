using FluentAssertions;
using Yumsy_Backend.Features.Posts.GetNewPosts;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class GetNewPostsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetNewPostsHandler(context);
        var request = new GetNewPostsRequest
        {
            UserId = Guid.NewGuid()
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Posts.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnPosts_When_RecentPostsExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new GetNewPostsHandler(context);
        var request = new GetNewPostsRequest
        {
            UserId = user.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Posts.Should().HaveCount(1);
        result.Posts.First().Id.Should().Be(post.Id);
    }

    [Fact]
    public async Task Handle_Should_MarkPostAsLiked_When_UserLikedIt()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var like = TestDataBuilder.CreateLike(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Likes.Add(like);
        await context.SaveChangesAsync();

        var handler = new GetNewPostsHandler(context);
        var request = new GetNewPostsRequest
        {
            UserId = user.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Posts.First().IsLiked.Should().BeTrue();
    }
}
