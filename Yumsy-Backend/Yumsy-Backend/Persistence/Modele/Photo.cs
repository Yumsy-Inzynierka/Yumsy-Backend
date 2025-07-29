using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Photo
{
    public Guid Id { get; set; }

    public string CloudKey { get; set; } = null!;

    public Guid PostId { get; set; }

    public Guid StepId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual Step Step { get; set; } = null!;
}
