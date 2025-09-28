using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("ingredient")]
public class Ingredient
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }

    public string? Brand { get; set; }

    public string? MainCategory { get; set; }

    public decimal EnergyKcal100g { get; set; }

    public decimal Fat100g { get; set; }

    public decimal Carbohydrates100g { get; set; }

    public decimal? Fiber100g { get; set; }

    public decimal? Sugars100g { get; set; }

    public decimal Proteins100g { get; set; }

    public decimal? Salt100g { get; set; }

    public decimal SearchScore { get; set; } = 0.0m;

    public ICollection<IngredientPost> IngredientPosts { get; set; } = new HashSet<IngredientPost>();

    public ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new HashSet<IngredientShoppingList>();
}