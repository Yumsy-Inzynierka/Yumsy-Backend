using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("top_daily_post")]
public class TopDailyPost
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("post_id")]
    public Guid PostId { get; set; }
    
    [Required]
    [Column("date")]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(1, 8)]
    [Column("rank")]
    public int Rank { get; set; }
    
    [ForeignKey(nameof(PostId))] 
    public Post Post { get; set; }
}