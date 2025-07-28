using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class Recommendation : BaseModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<RecommendationTag> RecommendationTags { get; set; } = new List<RecommendationTag>();

    public virtual User User { get; set; } = null!;
}
