namespace Yumsy_Backend.Shared.EventLogger;

public interface IAppEventLogger
{
    Task LogAsync(string action, Guid? userId, Guid? entityId);
}