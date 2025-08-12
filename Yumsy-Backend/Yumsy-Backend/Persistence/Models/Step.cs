using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("step")]
public class Step
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(300)]
    public string Description { get; set; }
    public int StepNumber { get; set; }
    public Photo? Photo { get; set; }
}