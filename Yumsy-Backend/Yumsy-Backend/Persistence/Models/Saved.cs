using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("saved")]
[PrimaryKey(nameof(UserId), nameof(PostId))]
public class Saved
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("post_id")]
    public Guid PostId { get; set; }

    [Column("saved_at")]
    public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}