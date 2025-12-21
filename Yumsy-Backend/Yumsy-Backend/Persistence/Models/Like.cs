using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("like")]
[PrimaryKey(nameof(UserId), nameof(PostId))]
public class Like
{
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Column("post_id")]
    public Guid PostId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}