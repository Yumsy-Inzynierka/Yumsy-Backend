using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class CommentLike : BaseModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CommentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
