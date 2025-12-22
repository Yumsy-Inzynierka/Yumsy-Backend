using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("tag")]
public class Tag
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    [Column("name")]
    public string Name { get; set; } = null!;

    [MaxLength(300)]
    [Column("emote")]
    public string? Emote { get; set; }
    
    [Column("tag_category_id")]
    public Guid TagCategoryId { get; set; }
    
    [ForeignKey(nameof(TagCategoryId))]
    public TagCategory TagCategory { get; set; }

    public ICollection<QuizAnswer> QuizAnswers { get; set; } = new HashSet<QuizAnswer>();

    public ICollection<Recommendation> Recommendations { get; set; } = new HashSet<Recommendation>();
    public ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
}