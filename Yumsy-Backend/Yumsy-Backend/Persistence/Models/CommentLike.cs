using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("comment_like")]
public class CommentLike
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("comment_id")]
    public Guid CommentId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(CommentId))]
    public Comment Comment { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}