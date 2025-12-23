using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("app_event_log")]
public class AppEventLog
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    [Column("user_id")]
    public Guid? UserId { get; set; }

    [Column("entity_id")]
    public Guid? EntityId { get; set; }

    [Column("action")]
    public string Action { get; set; } = null!;

    [Column("result")]
    public string Result { get; set; } = null!;
}