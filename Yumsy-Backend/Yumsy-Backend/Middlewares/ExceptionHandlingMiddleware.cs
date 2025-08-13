using System.Net;
using System.Security;
using System.Text.Json;
using FluentValidation;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred");

            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Message = ex.Message,
                ExceptionType = ex.GetType().Name
            };

            context.Response.StatusCode = ex switch
            {
            // 400 - Błąd w danych wejściowych lub parametrach
            ArgumentNullException => (int)HttpStatusCode.BadRequest,         // Wymagany parametr jest null
            ArgumentException => (int)HttpStatusCode.BadRequest,             // Niepoprawny argument (np. putsy string czy coś innego) różni się od validationException tym, że to jest wewnątrz w kodzie, a validationException tylko do FluentValidation
            FormatException => (int)HttpStatusCode.BadRequest,               // Nie udało się sparsować danych (np. string -> int)

            // 401 - Brak autoryzacji
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // Próba dostępu bez uwierzytelnienia

            // 403 - Brak uprawnień
            SecurityException => (int)HttpStatusCode.Forbidden,              // Użytkownik zalogowany, ale brak mu uprawnień

            // 404 - Nie znaleziono zasobu
            KeyNotFoundException => (int)HttpStatusCode.NotFound,            // Brak elementu w kolekcji

            // 409 - Konflikt stanu
            InvalidOperationException => (int)HttpStatusCode.Conflict,       // Operacja w niepoprawnym stanie (coś ogólnie można zrobić ale nie w danym stanie obiektu)

            // 422 - Dane poprawne składniowo, ale niepoprawne semantycznie
            ValidationException => 422,                                     // Błąd walidacji danych (FluentValidation)

            // 500 - Wszystko inne, błąd wewnętrzny
            _ => (int)HttpStatusCode.InternalServerError
        };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }

    private class ErrorResponse
    {
        public string Message { get; set; }
        public string ExceptionType { get; set; }
        
        /*
         Przykładowy response:
         
         HTTP/1.1 400 Bad Request
         Content-Type: application/json
         {
            "message": "PostId must not be empty.",
            "exceptionType": "ArgumentException"
         }
        */
    }
}