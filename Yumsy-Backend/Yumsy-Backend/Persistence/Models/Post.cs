using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("post")]
public class Post
{
    [Key]
    public Guid Id { get; set; }
    
    [MaxLength(50)]
    public string Title { get; set; }

    public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    [MaxLength(400)]
    public string Description { get; set; }
    public int CookingTime { get; set; }

    public int LikesCount { get; set; }

    public int CommentsCount { get; set; }

    public int SavedCount { get; set; }

    public int SharedCount { get; set; }
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User CreatedBy { get; set; }

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    
    public ICollection<IngredientPost> IngredientPosts { get; set; } = new HashSet<IngredientPost>();

    public ICollection<Like> Likes { get; set; } = new HashSet<Like>();

    public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

    public ICollection<ShoppingList> ShoppingLists { get; set; } = new HashSet<ShoppingList>();
    public ICollection<Step> Steps { get; set; } = new HashSet<Step>();

    public ICollection<PostTag> posttags { get; set; } = new HashSet<PostTag>();
}