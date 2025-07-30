using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Recommendation
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public virtual ICollection<RecommendationTag> RecommendationTags { get; set; } = new List<RecommendationTag>();

    public virtual User User { get; set; } = null!;
}
