using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("shopping_list")]
public class ShoppingList
{
    [Key] 
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [MaxLength(50)]
    [Column("title")]
    public string Title { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("created_from_id")]
    public Guid? CreatedFromId { get; set; }
    
    [ForeignKey(nameof(CreatedFromId))]
    public Post CreatedFrom { get; set; }

    public ICollection<IngredientShoppingList> IngredientShoppingLists { get; set; } = new HashSet<IngredientShoppingList>();
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}