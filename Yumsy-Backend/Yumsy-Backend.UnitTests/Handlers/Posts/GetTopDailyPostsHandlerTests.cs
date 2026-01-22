using FluentAssertions;
using Yumsy_Backend.Features.Posts.GetTopDailyPosts;
using Yumsy_Backend.UnitTests.Fixtures;
using Yumsy_Backend.UnitTests.Helpers;

namespace Yumsy_Backend.UnitTests.Handlers.Posts;

public class GetTopDailyPostsHandlerTests
{
    [Fact]
    public async Task Handle_Should_ReturnEmptyList_When_NoTopDailyPosts()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var handler = new GetTopDailyPostsHandler(context);
        var request = new GetTopDailyPostsRequest
        {
            UserId = Guid.NewGuid()
        };

        var result = await handler.Handle(request, CancellationToken.None);

        result.Should().NotBeNull();
        result.Posts.Should().BeEmpty();
    }
}
