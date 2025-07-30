using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Tag
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Emote { get; set; }

    public virtual ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

    public virtual ICollection<RecommendationTag> RecommendationTags { get; set; } = new List<RecommendationTag>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
