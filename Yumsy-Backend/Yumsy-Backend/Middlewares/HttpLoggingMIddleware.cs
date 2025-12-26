using System.Diagnostics;
using System.Security.Claims;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Middlewares;

public class HttpLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public HttpLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, SupabaseDbContext dbContext)
    {
        var stopwatch = Stopwatch.StartNew();

        // 🔑 ONE CorrelationId PER REQUEST
        var correlationId = Guid.NewGuid();
        context.Items["CorrelationId"] = correlationId;

        // expose to client
        context.Response.Headers["Trace-Id"] = correlationId.ToString();

        await _next(context);

        stopwatch.Stop();

        Guid? userId = null;
        var sub = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (Guid.TryParse(sub, out var parsedUserId))
        {
            userId = parsedUserId;
        }

        var log = new HttpLog
        {
            Method = context.Request.Method,
            Path = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            Duration = (int)stopwatch.ElapsedMilliseconds,
            CorrelationId = correlationId,
            UserId = userId
        };

        dbContext.HttpLogs.Add(log);
        await dbContext.SaveChangesAsync();
    }
}