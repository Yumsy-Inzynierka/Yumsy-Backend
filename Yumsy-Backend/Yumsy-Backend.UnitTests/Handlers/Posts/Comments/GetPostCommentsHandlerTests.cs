using FluentAssertions;
using Yumsy_Backend.Features.Posts.Comments.GetPostComments;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts.Comments;

public class GetPostCommentsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_PostDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetPostCommentsHandler(context);
        var postId = Guid.NewGuid();
        var request = new GetPostCommentsRequest
        {
            UserId = Guid.NewGuid(),
            PostId = postId
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Post with ID: {postId} not found.");
    }

    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoComments()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        await context.SaveChangesAsync();

        var handler = new GetPostCommentsHandler(context);
        var request = new GetPostCommentsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Comments.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnComments_When_CommentsExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id, content: "Test comment");

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        await context.SaveChangesAsync();

        var handler = new GetPostCommentsHandler(context);
        var request = new GetPostCommentsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Comments.Should().HaveCount(1);
        result.Comments.First().Content.Should().Be("Test comment");
    }

    [Fact]
    public async Task Handle_Should_MarkCommentAsLiked_When_UserLikedIt()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var comment = TestDataBuilder.CreateComment(userId: user.Id, postId: post.Id, content: "Test comment");
        var commentLike = TestDataBuilder.CreateCommentLike(userId: user.Id, commentId: comment.Id);

        context.Users.Add(user);
        context.Posts.Add(post);
        context.Comments.Add(comment);
        context.CommentLikes.Add(commentLike);
        await context.SaveChangesAsync();

        var handler = new GetPostCommentsHandler(context);
        var request = new GetPostCommentsRequest
        {
            UserId = user.Id,
            PostId = post.Id
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Comments.First().IsLiked.Should().BeTrue();
    }
}
