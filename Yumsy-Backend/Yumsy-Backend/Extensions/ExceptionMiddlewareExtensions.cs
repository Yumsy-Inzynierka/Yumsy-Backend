using Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;
namespace Yumsy_Backend.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}