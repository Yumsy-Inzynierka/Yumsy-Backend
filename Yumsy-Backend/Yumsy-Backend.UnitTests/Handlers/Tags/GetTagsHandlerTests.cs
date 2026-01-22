using FluentAssertions;
using Yumsy_Backend.Features.Tags.GetTags;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Tags;

public class GetTagsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoCategories()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetTagsHandler(context);
        var request = new GetTagsRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Categories.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnCategories_When_CategoriesExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);

        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        await context.SaveChangesAsync();

        var handler = new GetTagsHandler(context);
        var request = new GetTagsRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        result.Categories.Should().HaveCount(1);
        result.Categories.First().Tags.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_Should_ReturnAllTagsInCategory()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var category = TestDataBuilder.CreateTagCategory();
        var tag1 = TestDataBuilder.CreateTag(tagCategoryId: category.Id);
        var tag2 = TestDataBuilder.CreateTag(tagCategoryId: category.Id);

        context.TagCategories.Add(category);
        context.Tags.AddRange(tag1, tag2);
        await context.SaveChangesAsync();

        var handler = new GetTagsHandler(context);
        var request = new GetTagsRequest();

        var result = await handler.Handle(request, CancellationToken.None);

        result.Categories.First().Tags.Should().HaveCount(2);
    }
}
