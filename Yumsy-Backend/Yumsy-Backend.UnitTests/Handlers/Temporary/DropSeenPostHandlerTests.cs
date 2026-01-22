using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Features.Temporary.DropSeenPost;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Temporary;

public class DropSeenPostHandlerTests
{
    [Fact]
    public async Task Handle_Should_DoNothing_When_NoSeenPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new DropSeenPostHandler(context);
        var request = new DropSeenPostRequest
        {
            UserId = Guid.NewGuid()
        };

        await handler.Handle(request);

        var seenPosts = await context.SeenPosts.ToListAsync();
        seenPosts.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_RemoveSeenPosts_When_SeenPostsExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post1 = TestDataBuilder.CreatePost(userId: user.Id);
        var post2 = TestDataBuilder.CreatePost(userId: user.Id);
        var seenPost1 = TestDataBuilder.CreateSeenPost(userId: user.Id, postId: post1.Id);
        var seenPost2 = TestDataBuilder.CreateSeenPost(userId: user.Id, postId: post2.Id);

        context.Users.Add(user);
        context.Posts.AddRange(post1, post2);
        context.SeenPosts.AddRange(seenPost1, seenPost2);
        await context.SaveChangesAsync();

        var handler = new DropSeenPostHandler(context);
        var request = new DropSeenPostRequest
        {
            UserId = user.Id
        };

        await handler.Handle(request);

        var remainingSeenPosts = await context.SeenPosts
            .Where(sp => sp.UserId == user.Id)
            .ToListAsync();
        remainingSeenPosts.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_OnlyRemoveSeenPostsForSpecificUser()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user1 = TestDataBuilder.CreateUser();
        var user2 = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user1.Id);
        var seenPostUser1 = TestDataBuilder.CreateSeenPost(userId: user1.Id, postId: post.Id);
        var seenPostUser2 = TestDataBuilder.CreateSeenPost(userId: user2.Id, postId: post.Id);

        context.Users.AddRange(user1, user2);
        context.Posts.Add(post);
        context.SeenPosts.AddRange(seenPostUser1, seenPostUser2);
        await context.SaveChangesAsync();

        var handler = new DropSeenPostHandler(context);
        var request = new DropSeenPostRequest
        {
            UserId = user1.Id
        };

        await handler.Handle(request);

        var remainingSeenPosts = await context.SeenPosts.ToListAsync();
        remainingSeenPosts.Should().HaveCount(1);
        remainingSeenPosts.First().UserId.Should().Be(user2.Id);
    }
}
