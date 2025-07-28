using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class RecommendationTag : BaseModel
{
    public Guid RecommendationId { get; set; }

    public Guid TagId { get; set; }

    public int Count { get; set; }

    public virtual Recommendation Recommendation { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
