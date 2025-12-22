using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("ingredient")]
public class Ingredient
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("name")]
    public string Name { get; set; }

    [Column("brand")]
    public string? Brand { get; set; }

    [Column("main_category")]
    public string? MainCategory { get; set; }

    [Column("energy_kcal_100g")]
    public decimal EnergyKcal100g { get; set; }

    [Column("fat_100g")]
    public decimal Fat100g { get; set; }

    [Column("carbohydrates_100g")]
    public decimal Carbohydrates100g { get; set; }

    [Column("fiber_100g")]
    public decimal? Fiber100g { get; set; }

    [Column("sugars_100g")]
    public decimal? Sugars100g { get; set; }

    [Column("proteins_100g")]
    public decimal Proteins100g { get; set; }

    [Column("salt_100g")]
    public decimal? Salt100g { get; set; }

    [Column("search_count")]
    public int SearchCount { get; set; } = 0;

    public ICollection<IngredientPost> IngredientPosts { get; set; } = new HashSet<IngredientPost>();

    public ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new HashSet<IngredientShoppingList>();
}