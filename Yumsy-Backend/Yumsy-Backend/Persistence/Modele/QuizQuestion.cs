using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class QuizQuestion
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public Guid TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;
}
