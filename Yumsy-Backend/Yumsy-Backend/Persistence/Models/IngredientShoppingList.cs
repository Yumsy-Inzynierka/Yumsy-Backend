using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("ingredient_shopping_list")]
[PrimaryKey(nameof(IngredientId), nameof(ShoppingListId))]
public class IngredientShoppingList
{
    [Column("shopping_list_id")]
    public Guid ShoppingListId { get; set; }
    
    [Column("ingredient_id")]
    public Guid IngredientId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }
    
    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; }

    [ForeignKey(nameof(ShoppingListId))]
    public ShoppingList ShoppingList { get; set; }
}