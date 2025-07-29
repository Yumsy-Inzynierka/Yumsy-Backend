using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class IngredientShoppingList : BaseModel
{
    public Guid ShoppingListId { get; set; }

    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual ShoppingList ShoppingList { get; set; } = null!;
}
