using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("http_log")]
public class HttpLog
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Method { get; set; }
    public string Path { get; set; }
    public int StatusCode { get; set; }
    public int Duration { get; set; }
    public Guid? UserId { get; set; }
    public Guid CorrelationId { get; set; }
}