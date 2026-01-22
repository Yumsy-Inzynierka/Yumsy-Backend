using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.UnitTests.Fixtures;

/// <summary>
/// Test-specific DbContext that bypasses the Npgsql configuration in OnConfiguring.
/// </summary>
public class TestSupabaseDbContext : SupabaseDbContext
{
    public TestSupabaseDbContext(DbContextOptions<SupabaseDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Do not call base.OnConfiguring() to avoid adding Npgsql provider
        // The InMemory provider is already configured via the constructor options
    }
}

public class DbContextFixture : IDisposable
{
    public SupabaseDbContext DbContext { get; }

    public DbContextFixture()
    {
        var options = new DbContextOptionsBuilder<SupabaseDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        DbContext = new TestSupabaseDbContext(options);
        DbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        DbContext.Database.EnsureDeleted();
        DbContext.Dispose();
    }
}

public static class DbContextFixtureExtensions
{
    public static SupabaseDbContext CreateFreshContext()
    {
        var options = new DbContextOptionsBuilder<SupabaseDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        var context = new TestSupabaseDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
