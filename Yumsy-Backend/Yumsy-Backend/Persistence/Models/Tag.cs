using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("tag")]
public class Tag
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(300)]
    public string? Emote { get; set; }

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = new HashSet<QuizQuestion>();

    public ICollection<Recommendation> Recommendations { get; set; } = new HashSet<Recommendation>();
    public ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
}