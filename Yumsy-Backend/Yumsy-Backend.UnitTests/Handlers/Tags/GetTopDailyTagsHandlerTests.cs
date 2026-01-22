using FluentAssertions;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Tags;

public class GetTopDailyTagsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoTopDailyTags()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetTopDailyTagsHandler(context);

        var result = await handler.Handle(CancellationToken.None);

        result.Should().NotBeNull();
        result.Tags.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnTags_When_TopDailyTagsExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var topDailyTag = TestDataBuilder.CreateTopDailyTag(tagId: tag.Id, rank: 1);

        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        context.TopDailyTags.Add(topDailyTag);
        await context.SaveChangesAsync();

        var handler = new GetTopDailyTagsHandler(context);

        var result = await handler.Handle(CancellationToken.None);

        result.Tags.Should().HaveCount(1);
        result.Tags.First().Id.Should().Be(tag.Id);
    }

    [Fact]
    public async Task Handle_Should_ReturnTagsOrderedByRank()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var category = TestDataBuilder.CreateTagCategory();
        var tag1 = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var tag2 = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var topDailyTag1 = TestDataBuilder.CreateTopDailyTag(tagId: tag1.Id, rank: 2);
        var topDailyTag2 = TestDataBuilder.CreateTopDailyTag(tagId: tag2.Id, rank: 1);

        context.TagCategories.Add(category);
        context.Tags.AddRange(tag1, tag2);
        context.TopDailyTags.AddRange(topDailyTag1, topDailyTag2);
        await context.SaveChangesAsync();

        var handler = new GetTopDailyTagsHandler(context);

        var result = await handler.Handle(CancellationToken.None);

        result.Tags.Should().HaveCount(2);
        result.Tags.First().Id.Should().Be(tag2.Id);
    }
}
