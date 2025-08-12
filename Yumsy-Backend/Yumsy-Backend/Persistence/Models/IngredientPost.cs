using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("ingredient_post")]
[PrimaryKey(nameof(IngredientId), nameof(PostId))]
public class IngredientPost
{
    public Guid PostId { get; set; }
    public Guid IngredientId { get; set; }

    public int Quantity { get; set; }
    
    [ForeignKey(nameof(IngredientId))]
    public Ingredient Ingredient { get; set; }
    
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}