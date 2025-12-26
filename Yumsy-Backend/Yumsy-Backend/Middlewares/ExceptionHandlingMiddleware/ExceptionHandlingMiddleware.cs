using System.Text.Json;
using FluentValidation;
using System.Security.Claims;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IExceptionStatusCodeMapper _statusCodeMapper;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        IExceptionStatusCodeMapper statusCodeMapper)
    {
        _next = next;
        _statusCodeMapper = statusCodeMapper;
    }

    public async Task Invoke(HttpContext context, SupabaseDbContext dbContext)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var statusCode = _statusCodeMapper.Map(ex);

            var correlationId =
                context.Items.TryGetValue("CorrelationId", out var value) && value is Guid guid
                    ? guid
                    : Guid.Empty;

            Guid? userId = null;
            var sub = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(sub, out var parsedUserId))
            {
                userId = parsedUserId;
            }

            var errorLog = new ErrorLog
            {
                Path = context.Request.Path,
                ExceptionType = ex.GetType().Name,
                Message = ex.Message,
                StackTrace = ex.StackTrace ?? string.Empty,
                CorrelationId = correlationId,
                UserId = userId
            };

            dbContext.ErrorLogs.Add(errorLog);
            await dbContext.SaveChangesAsync();

            var response = new ErrorResponse
            {
                TraceId = correlationId,
                ExceptionType = ex.GetType().Name,
                Message = ex is ValidationException
                    ? "Validation failed"
                    : ex.Message,
                Errors = ex is ValidationException validationEx
                    ? validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                    : null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private class ErrorResponse
    {
        public Guid TraceId { get; set; }
        public string ExceptionType { get; set; } = null!;
        public string Message { get; set; } = null!;
        public List<string>? Errors { get; set; }
    }
}
