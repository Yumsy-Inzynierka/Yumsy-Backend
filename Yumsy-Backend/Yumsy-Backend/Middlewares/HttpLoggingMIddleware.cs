using System.Diagnostics;
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
        var sw = Stopwatch.StartNew();
        var correlationId = Guid.NewGuid();

        context.Items["CorrelationId"] = correlationId;

        await _next(context);

        sw.Stop();

        var log = new HttpLog
        {
            Method = context.Request.Method,
            Path = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            Duration= (int)sw.ElapsedMilliseconds,
            CorrelationId = correlationId,
            UserId = context.User.Identity?.IsAuthenticated == true
                ? Guid.Parse(context.User.Identity.Name)
                : null,
        };

        dbContext.HttpLogs.Add(log);
        await dbContext.SaveChangesAsync();
    }
}
