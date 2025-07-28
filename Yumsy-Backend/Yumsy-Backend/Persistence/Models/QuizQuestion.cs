using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class QuizQuestion : BaseModel
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public string Name { get; set; } = null!;

    public Guid TagId { get; set; }

    public virtual Tag Tag { get; set; } = null!;
}
