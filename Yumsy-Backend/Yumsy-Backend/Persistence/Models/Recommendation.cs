using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("recommendation")]
[PrimaryKey(nameof(TagId), nameof(UserId))]
public class Recommendation
{
    [Column("tag_id")]
    public Guid TagId { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Column("score")]
    public int Score { get; set; }
}