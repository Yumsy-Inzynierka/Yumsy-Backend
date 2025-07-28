using System;
using System.Collections.Generic;
using Supabase.Postgrest.Models;

namespace Yumsy_Backend.Persistence.Models;

public partial class Ingredient : BaseModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Brand { get; set; }

    public string? MainCategory { get; set; }

    public decimal EnergyKcal100g { get; set; }

    public decimal Fat100g { get; set; }

    public decimal Carbohydrates100g { get; set; }

    public decimal? Fiber100g { get; set; }

    public decimal? Sugars100g { get; set; }

    public decimal Proteins100g { get; set; }

    public decimal? Salt100g { get; set; }

    public int SearchCount { get; set; }

    public virtual ICollection<IngredientPost> IngredientPosts { get; set; } = new List<IngredientPost>();

    public virtual ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new List<IngredientShoppingList>();
}
