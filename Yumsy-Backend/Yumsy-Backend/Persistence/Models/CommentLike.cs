using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class CommentLike
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CommentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Comment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
