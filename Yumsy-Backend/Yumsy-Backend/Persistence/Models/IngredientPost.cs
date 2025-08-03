using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class IngredientPost
{
    public Guid PostId { get; set; }

    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual Post Post { get; set; } = null!;
}
