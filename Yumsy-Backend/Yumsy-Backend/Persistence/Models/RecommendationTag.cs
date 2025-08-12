using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("recommendation_tag")]
[PrimaryKey(nameof(RecommendationId), nameof(TagId))]
public class RecommendationTag
{
    public Guid RecommendationId { get; set; }
    public Guid TagId { get; set; }

    public int Count { get; set; }

    [ForeignKey(nameof(RecommendationId))]
    public Recommendation Recommendation { get; set; }

    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
}