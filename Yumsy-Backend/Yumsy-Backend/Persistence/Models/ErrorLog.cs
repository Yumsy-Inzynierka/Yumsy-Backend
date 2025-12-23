using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("error_log")]
public class ErrorLog
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [Column("path")]
    public string Path { get; set; } = null!;

    [Column("exception_type")]
    public string ExceptionType { get; set; } = null!;

    [Column("status_code")]
    public int StatusCode { get; set; }

    [Column("message")]
    public string Message { get; set; } = null!;

    [Column("stack_trace")]
    public string StackTrace { get; set; } = null!;

    [Column("correlation_id")]
    public Guid CorrelationId { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }
}