using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class RecommendationTag
{
    public Guid RecommendationId { get; set; }

    public Guid TagId { get; set; }

    public int Count { get; set; }

    public virtual Recommendation Recommendation { get; set; } = null!;

    public virtual Tag Tag { get; set; } = null!;
}
