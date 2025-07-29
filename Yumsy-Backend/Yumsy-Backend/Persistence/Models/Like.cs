using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class Like : BaseModel
{
    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
