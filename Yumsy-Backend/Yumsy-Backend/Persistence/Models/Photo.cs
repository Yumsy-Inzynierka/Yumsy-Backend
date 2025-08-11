using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("photo")]
public class Photo
{
    [Key]
    public Guid Id { get; set; }
    public string ImageUrl { get; set; }
    public Guid StepId { get; set; }

    [ForeignKey(nameof(StepId))]
    public Step Step { get; set; }
}