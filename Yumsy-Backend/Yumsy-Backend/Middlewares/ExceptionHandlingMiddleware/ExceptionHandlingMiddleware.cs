using System.Text.Json;
using FluentValidation;
namespace Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IExceptionStatusCodeMapper _statusCodeMapper;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger,
        IExceptionStatusCodeMapper statusCodeMapper)
    {
        _next = next;
        _logger = logger;
        _statusCodeMapper = statusCodeMapper;
    }

    public async Task Invoke(HttpContext context)
    {
        var traceId = Guid.NewGuid().ToString();
        context.Response.Headers["Trace-Id"] = traceId;

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var statusCode = _statusCodeMapper.Map(ex);

            var response = new ErrorResponse
            {
                TraceId = traceId,
                ExceptionType = ex.GetType().Name,
                Message = ex is ValidationException ? "Validation failed" : ex.Message,
                Errors = ex is ValidationException validationEx 
                    ? validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                    : null
            };

            if (statusCode >= 500)
                _logger.LogCritical(ex, "Critical server error. TraceId: {TraceId}. Response: {@Response}", traceId, response);
            else if (statusCode >= 400)
                _logger.LogWarning(ex, "Client/validation error. TraceId: {TraceId}. Response: {@Response}", traceId, response);
            else
                _logger.LogError(ex, "Unhandled error. TraceId: {TraceId}. Response: {@Response}", traceId, response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private class ErrorResponse
    {
        public string TraceId { get; set; }
        public string ExceptionType { get; set; }
        public string Message { get; set; }
        public List<string>? Errors { get; set; }
    }
}
