using System.Net;
using System.Security;
using FluentValidation;
namespace Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;

public class ExceptionStatusCodeMapper : IExceptionStatusCodeMapper
{
    public int Map(Exception ex)
    {
        return ex switch
        {
            // 400
            ArgumentNullException => (int)HttpStatusCode.BadRequest,
            ArgumentException => (int)HttpStatusCode.BadRequest,
            FormatException => (int)HttpStatusCode.BadRequest,

            // 401
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,

            // 403
            SecurityException => (int)HttpStatusCode.Forbidden,

            // 404
            KeyNotFoundException => (int)HttpStatusCode.NotFound,

            // 409
            InvalidOperationException => (int)HttpStatusCode.Conflict,

            // 422
            ValidationException => 422,

            // 500 – fallback
            _ => (int)HttpStatusCode.InternalServerError
        };
    }
}