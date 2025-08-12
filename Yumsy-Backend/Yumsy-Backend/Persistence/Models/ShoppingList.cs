using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("shopping_list")]
public class ShoppingList
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Title { get; set; }

    public Guid UserId { get; set; }

    public Guid CreatedFromId { get; set; }
    
    [ForeignKey(nameof(CreatedFromId))]
    public Post CreatedFrom { get; set; }

    public ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new HashSet<IngredientShoppingList>();
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}