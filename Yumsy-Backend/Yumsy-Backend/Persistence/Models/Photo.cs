using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class Photo : BaseModel
{
    public Guid Id { get; set; }

    public string CloudKey { get; set; } = null!;

    public Guid PostId { get; set; }

    public Guid StepId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Step Step { get; set; } = null!;
}
