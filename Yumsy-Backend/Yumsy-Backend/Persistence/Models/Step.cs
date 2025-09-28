using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("step")]
public class Step
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [MaxLength(300)]
    public string Description { get; set; }
    public int StepNumber { get; set; }
    public string? ImageUrl { get; set; }
    public Guid PostId { get; set; }
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}