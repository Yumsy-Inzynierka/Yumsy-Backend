using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("recommendation")]
public class Recommendation
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }

    public ICollection<RecommendationTag> RecommendationTags { get; set; } = new HashSet<RecommendationTag>();
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}