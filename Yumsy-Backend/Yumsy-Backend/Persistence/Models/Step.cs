using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("step")]
public class Step
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(300)]
    [Column("description")]
    public string Description { get; set; }
    
    [Column("step_number")]
    public int StepNumber { get; set; }
    
    [Column("image_url")]
    public string? ImageUrl { get; set; }
    
    [Column("post_id")]
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}