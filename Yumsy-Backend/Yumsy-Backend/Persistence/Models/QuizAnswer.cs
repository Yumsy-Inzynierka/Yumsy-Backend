using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("quiz_answer")]
public class QuizAnswer
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("answer")]
    public string Answer { get; set; }
    
    [Column("quiz_question_id")]
    public Guid QuizQuestionId { get; set; }
    
    [ForeignKey(nameof(QuizQuestionId))]
    public QuizQuestion QuizQuestion { get; set; }
    
    [Column("tag_id")]
    public Guid? TagId { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag? Tag { get; set; }
}