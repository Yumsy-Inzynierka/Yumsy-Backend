using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("http_log")]
public class HttpLog
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [Column("method")]
    public string Method { get; set; } = null!;

    [Column("path")]
    public string Path { get; set; } = null!;

    [Column("status_code")]
    public int StatusCode { get; set; }

    [Column("duration_ms")]
    public int Duration { get; set; }

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("correlation_id")]
    public Guid CorrelationId { get; set; }
}