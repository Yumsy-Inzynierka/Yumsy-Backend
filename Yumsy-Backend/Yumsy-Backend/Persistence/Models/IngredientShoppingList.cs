using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class IngredientShoppingList
{
    public Guid ShoppingListId { get; set; }

    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual ShoppingList ShoppingList { get; set; } = null!;
}
