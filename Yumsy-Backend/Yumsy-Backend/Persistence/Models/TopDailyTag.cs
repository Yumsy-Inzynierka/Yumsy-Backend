using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("top_daily_tag")]
public class TopDailyTag
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("tag_id")]
    public Guid TagId { get; set; }
    
    [Required]
    [Column("date")]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(1, 8)]
    [Column("rank")]
    public int Rank { get; set; }
    
    [ForeignKey(nameof(TagId))] 
    public Tag Tag { get; set; }
}