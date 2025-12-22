using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Shared.EventLogger;

public class AppEventLogger : IAppEventLogger
{
    private readonly SupabaseDbContext _dbContext;

    public AppEventLogger(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task LogAsync(string action, Guid? userId, Guid? entityId, string result)
    {
        _dbContext.AppEventLogs.Add(new AppEventLog
        {
            Action = action,
            EntityId = entityId,
            UserId = userId,
            Result = result
        });

        await _dbContext.SaveChangesAsync();
    }
}