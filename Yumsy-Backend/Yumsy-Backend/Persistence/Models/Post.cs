using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("post")]
public class Post
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [MaxLength(50)]
    [Column("title")]
    public string Title { get; set; }

    [Column("posted_date")]
    public DateTime PostedDate { get; set; } = DateTime.UtcNow;
    
    [MaxLength(400)]
    [Column("description")]
    public string Description { get; set; }
    
    [Column("cooking_time")]
    public int CookingTime { get; set; }

    [Column("likes_count")]
    public int LikesCount { get; set; }

    [Column("comments_count")]
    public int CommentsCount { get; set; }

    [Column("saved_count")]
    public int SavedCount { get; set; }

    [Column("shared_count")]
    public int SharedCount { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User CreatedBy { get; set; }

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    
    public ICollection<IngredientPost> IngredientPosts { get; set; } = new HashSet<IngredientPost>();

    public ICollection<Like> Likes { get; set; } = new HashSet<Like>();

    public ICollection<PostImage> PostImages { get; set; } = new HashSet<PostImage>();

    public ICollection<ShoppingList> ShoppingLists { get; set; } = new HashSet<ShoppingList>();
    public ICollection<Step> Steps { get; set; } = new HashSet<Step>();

    public ICollection<PostTag> PostTags { get; set; } = new HashSet<PostTag>();
    
    public ICollection<Saved> Saved { get; set; } = new HashSet<Saved>();

}