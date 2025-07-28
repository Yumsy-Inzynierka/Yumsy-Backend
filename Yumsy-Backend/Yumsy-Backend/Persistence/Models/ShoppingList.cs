using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class ShoppingList : BaseModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid CreatedFrom { get; set; }

    public virtual Post CreatedFromNavigation { get; set; } = null!;

    public virtual ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new List<IngredientShoppingList>();

    public virtual User User { get; set; } = null!;
}
