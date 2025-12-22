using Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;
namespace Yumsy_Backend.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}