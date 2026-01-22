using FluentAssertions;
using Yumsy_Backend.Features.Posts.GetPostsByTag;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class GetPostsByTagHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoPostsWithTag()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetPostsByTagHandler(context);
        var request = new GetPostsByTagRequest
        {
            TagId = Guid.NewGuid(),
            CurrentPage = 1
        };

        var result = await handler.Handle(request, Guid.NewGuid(), CancellationToken.None);

        result.Should().NotBeNull();
        result.Posts.Should().BeEmpty();
        result.HasMore.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnPosts_When_PostsWithTagExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var post = TestDataBuilder.CreatePost(userId: user.Id);
        var postTag = TestDataBuilder.CreatePostTag(postId: post.Id, tagId: tag.Id);

        context.Users.Add(user);
        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        context.Posts.Add(post);
        context.PostTags.Add(postTag);
        await context.SaveChangesAsync();

        var handler = new GetPostsByTagHandler(context);
        var request = new GetPostsByTagRequest
        {
            TagId = tag.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.Posts.Should().HaveCount(1);
        result.Posts.First().Id.Should().Be(post.Id);
    }

    [Fact]
    public async Task Handle_Should_OrderByPopularity()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var lessPopularPost = TestDataBuilder.CreatePost(userId: user.Id, likesCount: 1, commentsCount: 0, savedCount: 0);
        var morePopularPost = TestDataBuilder.CreatePost(userId: user.Id, likesCount: 10, commentsCount: 5, savedCount: 3);
        var postTag1 = TestDataBuilder.CreatePostTag(postId: lessPopularPost.Id, tagId: tag.Id);
        var postTag2 = TestDataBuilder.CreatePostTag(postId: morePopularPost.Id, tagId: tag.Id);

        context.Users.Add(user);
        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        context.Posts.AddRange(lessPopularPost, morePopularPost);
        context.PostTags.AddRange(postTag1, postTag2);
        await context.SaveChangesAsync();

        var handler = new GetPostsByTagHandler(context);
        var request = new GetPostsByTagRequest
        {
            TagId = tag.Id,
            CurrentPage = 1
        };

        var result = await handler.Handle(request, user.Id, CancellationToken.None);

        result.Posts.Should().HaveCount(2);
        result.Posts.First().Id.Should().Be(morePopularPost.Id);
    }
}
