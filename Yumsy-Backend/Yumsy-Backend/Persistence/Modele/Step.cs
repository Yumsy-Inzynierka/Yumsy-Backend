using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Step
{
    public Guid Id { get; set; }

    public string Description { get; set; } = null!;

    public Guid PostId { get; set; }

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual Post Post { get; set; } = null!;
}
