using FluentAssertions;
using Yumsy_Backend.Features.Posts.EditPost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class EditPostHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new EditPostHandler(context);
        var postId = Guid.NewGuid();
        var request = new EditPostRequest
        {
            UserId = Guid.NewGuid(),
            PostId = postId,
            Body = new EditPostRequestBody
            {
                Title = "Updated Title",
                Description = "Updated Description"
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {postId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ThrowUnauthorizedAccessException_When_UserIsNotOwner()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var otherUser = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.AddRange(user, otherUser);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new EditPostHandler(context);
        var request = new EditPostRequest
        {
            UserId = otherUser.Id,
            PostId = post.Id,
            Body = new EditPostRequestBody
            {
                Title = "Updated Title",
                Description = "Updated Description"
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<UnauthorizedAccessException>()
            .WithMessage($"User with ID: {otherUser.Id} is not the owner of this post.");
    }

    [Fact]
    public async Task Handle_Should_UpdatePost_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new EditPostHandler(context);
        var request = new EditPostRequest
        {
            UserId = user.Id,
            PostId = post.Id,
            Body = new EditPostRequestBody
            {
                Title = "Updated Title",
                Description = "Updated Description"
            }
        };

        await handler.Handle(request, CancellationToken.None);

        var updatedPost = await context.Posts.FindAsync(post.Id);
        updatedPost!.Title.Should().Be("Updated Title");
        updatedPost.Description.Should().Be("Updated Description");
    }
}
