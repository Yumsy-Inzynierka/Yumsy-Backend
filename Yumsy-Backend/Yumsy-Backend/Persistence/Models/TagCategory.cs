using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("tag_category")]
public class TagCategory
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(50)]
    public string Name { get; set; }

    public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
    public ICollection<QuizQuestion> QuizQuestions { get; set; } = new HashSet<QuizQuestion>();
}