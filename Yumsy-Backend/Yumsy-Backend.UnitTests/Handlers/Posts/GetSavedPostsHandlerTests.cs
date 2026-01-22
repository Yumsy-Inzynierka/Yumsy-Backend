using FluentAssertions;
using Yumsy_Backend.Features.Posts.GetSavedPosts;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class GetSavedPostsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoSavedPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new GetSavedPostsHandler(context);
        var request = new GetSavedPostsRequest
        {
            UserId = user.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.Should().NotBeNull();
        result.SavedPosts.Should().BeEmpty();
        result.HasMore.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnSavedPosts_When_UserHasSavedPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var saved = TestDataBuilder.CreateSaved(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Saved.Add(saved);
        await context.SaveChangesAsync();

        var handler = new GetSavedPostsHandler(context);
        var request = new GetSavedPostsRequest
        {
            UserId = user.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.SavedPosts.Should().HaveCount(1);
        result.SavedPosts.First().Id.Should().Be(post.Id);
    }
}
