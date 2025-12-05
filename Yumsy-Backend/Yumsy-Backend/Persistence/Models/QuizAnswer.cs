using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("quiz_answer")]
public class QuizAnswer
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Answer { get; set; }
    public Guid QuizQuestionId { get; set; }
    
    [ForeignKey(nameof(QuizQuestionId))]
    public QuizQuestion QuizQuestion { get; set; }

    public Guid? TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag? Tag { get; set; }
}