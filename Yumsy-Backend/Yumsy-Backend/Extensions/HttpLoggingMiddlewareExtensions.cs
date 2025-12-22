using Yumsy_Backend.Middlewares;

namespace Yumsy_Backend.Extensions;

public static class HttpLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalHttpLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpLoggingMiddleware>();
    }
}
