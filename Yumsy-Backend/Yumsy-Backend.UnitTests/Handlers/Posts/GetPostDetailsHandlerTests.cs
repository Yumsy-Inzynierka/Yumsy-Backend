using FluentAssertions;
using Yumsy_Backend.Features.Posts.GetPostDetails;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class GetPostDetailsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetPostDetailsHandler(context);
        var postId = Guid.NewGuid();
        var request = new GetPostDetailsRequest
        {
            UserId = Guid.NewGuid(),
            PostId = postId
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {postId} does not exist.");
    }

    [Fact]
    public async Task Handle_Should_ReturnPostDetails_When_PostExists()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new GetPostDetailsHandler(context);
        var request = new GetPostDetailsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().Be(post.Id);
        result.Title.Should().Be(post.Title);
        result.UserId.Should().Be(user.Id);
    }

    [Fact]
    public async Task Handle_Should_ReturnIsLikedTrue_When_UserLikedPost()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var like = TestDataBuilder.CreateLike(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Likes.Add(like);
        await context.SaveChangesAsync();

        var handler = new GetPostDetailsHandler(context);
        var request = new GetPostDetailsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsLiked.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnIsSavedTrue_When_UserSavedPost()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var saved = TestDataBuilder.CreateSaved(userId: user.Id, postId: post.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Saved.Add(saved);
        await context.SaveChangesAsync();

        var handler = new GetPostDetailsHandler(context);
        var request = new GetPostDetailsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.IsSaved.Should().BeTrue();
    }
}
