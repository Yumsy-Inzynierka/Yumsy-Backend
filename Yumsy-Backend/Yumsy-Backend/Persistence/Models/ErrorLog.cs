using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("error_log")]
public class ErrorLog
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Path { get; set; }
    public string ExceptionType { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }
    public Guid CorrelationId { get; set; }
    public Guid? UserId { get; set; }
}