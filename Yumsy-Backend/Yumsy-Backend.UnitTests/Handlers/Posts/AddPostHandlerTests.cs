using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Yumsy_Backend.Features.Posts.AddPost;
using Yumsy_Backend.Shared.EventLogger;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class AddPostHandlerTests
{
    private readonly Mock<IAppEventLogger> _mockEventLogger;

    public AddPostHandlerTests()
    {
        _mockEventLogger = new Mock<IAppEventLogger>();
        _mockEventLogger
            .Setup(x => x.LogAsync(It.IsAny<string>(), It.IsAny<Guid?>(), It.IsAny<Guid?>()))
            .Returns(Task.CompletedTask);
    }

    [Fact]
    public async Task Handle_Should_ThrowKeyNotFoundException_When_UserDoesNotExist()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new AddPostHandler(context, _mockEventLogger.Object);
        var request = new AddPostRequest
        {
            UserId = Guid.NewGuid(),
            Body = new AddPostRequestBody
            {
                Title = "Test Recipe",
                Description = "Test Description",
                CookingTime = 30,
                Tags = Enumerable.Empty<AddPostRequestTag>(),
                Images = Enumerable.Empty<AddPostRequestImage>(),
                Ingredients = Enumerable.Empty<AddPostRequestIngredient>(),
                RecipeSteps = Enumerable.Empty<AddPostRequestRecipeStep>()
            }
        };

        var act = () => handler.Handle(request, CancellationToken.None);

        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage("User not found.");
    }

    [Fact]
    public async Task Handle_Should_CreatePost_When_RequestIsValid()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        var category = TestDataBuilder.CreateTagCategory();
        var tag = TestDataBuilder.CreateTag(tagCategoryId: category.Id);

        context.Users.Add(user);
        context.TagCategories.Add(category);
        context.Tags.Add(tag);
        await context.SaveChangesAsync();

        var handler = new AddPostHandler(context, _mockEventLogger.Object);
        var request = new AddPostRequest
        {
            UserId = user.Id,
            Body = new AddPostRequestBody
            {
                Title = "Test Recipe",
                Description = "Test Description",
                CookingTime = 30,
                Tags = new[] { new AddPostRequestTag { Id = tag.Id } },
                Images = new[] { new AddPostRequestImage { Image = "https://example.com/image.jpg" } },
                Ingredients = Enumerable.Empty<AddPostRequestIngredient>(),
                RecipeSteps = new[] { new AddPostRequestRecipeStep { StepNumber = 1, Description = "Step 1" } }
            }
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Id.Should().NotBeEmpty();

        var post = await context.Posts
            .Include(p => p.PostImages)
            .Include(p => p.Steps)
            .Include(p => p.PostTags)
            .FirstOrDefaultAsync(p => p.Id == result.Id);

        post.Should().NotBeNull();
        post!.Title.Should().Be("Test Recipe");
        post.PostImages.Should().HaveCount(1);
        post.Steps.Should().HaveCount(1);
        post.PostTags.Should().HaveCount(1);
    }

    [Fact]
    public async Task Handle_Should_LogEvent_When_PostIsCreated()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var user = TestDataBuilder.CreateUser();
        context.Users.Add(user);
        await context.SaveChangesAsync();

        var handler = new AddPostHandler(context, _mockEventLogger.Object);
        var request = new AddPostRequest
        {
            UserId = user.Id,
            Body = new AddPostRequestBody
            {
                Title = "Test Recipe",
                Description = "Test Description",
                Tags = Enumerable.Empty<AddPostRequestTag>(),
                Images = Enumerable.Empty<AddPostRequestImage>(),
                Ingredients = Enumerable.Empty<AddPostRequestIngredient>(),
                RecipeSteps = Enumerable.Empty<AddPostRequestRecipeStep>()
            }
        };

        var result = await handler.Handle(request, CancellationToken.None);

        _mockEventLogger.Verify(
            x => x.LogAsync("create_post", user.Id, result.Id),
            Times.Once);
    }
}
