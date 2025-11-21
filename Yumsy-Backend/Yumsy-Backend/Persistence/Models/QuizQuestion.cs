using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("quiz_question")]
public class QuizQuestion
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Question { get; set; }
    public bool Mandatory { get; set; }

    public Guid TagCategoryId { get; set; }
    
    [ForeignKey(nameof(TagCategoryId))]
    public TagCategory TagCategory { get; set; }
    
    public ICollection<QuizAnswer> QuizAnswers { get; set; } = new HashSet<QuizAnswer>();
}