using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("tag")]
public class Tag
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [MaxLength(300)]
    public string? Emote { get; set; }

    public ICollection<QuizQuestion> QuizQuestions { get; set; } = new HashSet<QuizQuestion>();

    public ICollection<RecommendationTag> RecommendationTags { get; set; } = new HashSet<RecommendationTag>();

    public ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
}