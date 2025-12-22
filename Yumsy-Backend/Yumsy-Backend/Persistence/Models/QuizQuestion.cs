using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("quiz_question")]
public class QuizQuestion
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("question")]
    public string Question { get; set; }
    
    [Column("mandatory")]
    public bool Mandatory { get; set; }

    [Column("tag_category_id")]
    public Guid TagCategoryId { get; set; }
    
    [ForeignKey(nameof(TagCategoryId))]
    public TagCategory TagCategory { get; set; }
    
    public ICollection<QuizAnswer> QuizAnswers { get; set; } = new HashSet<QuizAnswer>();
}