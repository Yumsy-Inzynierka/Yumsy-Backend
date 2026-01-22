using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Shared.EventLogger;
using Yumsy_Backend.UnitTests.Fixtures;

namespace Yumsy_Backend.UnitTests.Shared;

public class AppEventLoggerTests
{
    [Fact]
    public async Task LogAsync_Should_CreateEventLog_WithAllParameters()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        var action = "TestAction";
        var userId = Guid.NewGuid();
        var entityId = Guid.NewGuid();

        await logger.LogAsync(action, userId, entityId);

        var log = await context.AppEventLogs.FirstOrDefaultAsync();
        log.Should().NotBeNull();
        log!.Action.Should().Be(action);
        log.UserId.Should().Be(userId);
        log.EntityId.Should().Be(entityId);
    }

    [Fact]
    public async Task LogAsync_Should_CreateEventLog_WithNullUserId()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        var action = "TestAction";
        var entityId = Guid.NewGuid();

        await logger.LogAsync(action, null, entityId);

        var log = await context.AppEventLogs.FirstOrDefaultAsync();
        log.Should().NotBeNull();
        log!.Action.Should().Be(action);
        log.UserId.Should().BeNull();
        log.EntityId.Should().Be(entityId);
    }

    [Fact]
    public async Task LogAsync_Should_CreateEventLog_WithNullEntityId()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        var action = "TestAction";
        var userId = Guid.NewGuid();

        await logger.LogAsync(action, userId, null);

        var log = await context.AppEventLogs.FirstOrDefaultAsync();
        log.Should().NotBeNull();
        log!.Action.Should().Be(action);
        log.UserId.Should().Be(userId);
        log.EntityId.Should().BeNull();
    }

    [Fact]
    public async Task LogAsync_Should_CreateEventLog_WithAllNullOptionalParameters()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        var action = "TestAction";

        await logger.LogAsync(action, null, null);

        var log = await context.AppEventLogs.FirstOrDefaultAsync();
        log.Should().NotBeNull();
        log!.Action.Should().Be(action);
        log.UserId.Should().BeNull();
        log.EntityId.Should().BeNull();
    }

    [Fact]
    public async Task LogAsync_Should_SetTimestamp()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        var beforeTime = DateTime.UtcNow;
        await logger.LogAsync("TestAction", null, null);
        var afterTime = DateTime.UtcNow;

        var log = await context.AppEventLogs.FirstOrDefaultAsync();
        log.Should().NotBeNull();
        log!.Timestamp.Should().BeOnOrAfter(beforeTime);
        log.Timestamp.Should().BeOnOrBefore(afterTime);
    }

    [Fact]
    public async Task LogAsync_Should_CreateMultipleLogs()
    {
        using var context = DbContextFixtureExtensions.CreateFreshContext();
        var logger = new AppEventLogger(context);

        await logger.LogAsync("Action1", Guid.NewGuid(), null);
        await logger.LogAsync("Action2", null, Guid.NewGuid());
        await logger.LogAsync("Action3", Guid.NewGuid(), Guid.NewGuid());

        var logs = await context.AppEventLogs.ToListAsync();
        logs.Should().HaveCount(3);
    }
}
