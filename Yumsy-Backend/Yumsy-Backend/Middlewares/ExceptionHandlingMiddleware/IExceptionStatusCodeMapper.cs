namespace Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;

public interface IExceptionStatusCodeMapper
{
    int Map(Exception exception);
}
