using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("quiz_question")]
public class QuizQuestion
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string Description { get; set; }

    [MaxLength(40)]
    public string Name { get; set; }

    public Guid TagId { get; set; }
    
    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
}