using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("user")]
public class User
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [EmailAddress]
    [MaxLength(320)]
    [Column("email")]
    public string Email { get; set; }
    
    [MaxLength(20)]
    [Column("username")]
    public string Username { get; set; }
    
    [MaxLength(20)]
    [Column("profile_name")]
    public string? ProfileName { get; set; }
    
    [Column("profile_picture")]
    public string? ProfilePicture { get; set; }

    [MaxLength(400)]
    [Column("bio")]
    public string? Bio { get; set; }

    [Column("followers_count")]
    public int FollowersCount { get; set; }

    [Column("following_count")]
    public int FollowingCount { get; set; }

    [Column("recipes_count")]
    public int RecipesCount { get; set; }

    [Column("registration_date")]
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [MaxLength(20)]
    [Column("role")]
    public string Role { get; set; }

    public ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    public ICollection<Like> Likes { get; set; } = new HashSet<Like>();

    public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

    public ICollection<Recommendation> Recommendations { get; set; } = new HashSet<Recommendation>();

    public ICollection<ShoppingList> ShoppingLists { get; set; } = new HashSet<ShoppingList>();
    
    public ICollection<UserFollower> Followers { get; set; } = new HashSet<UserFollower>();
    public ICollection<UserFollower> Followings { get; set; } = new HashSet<UserFollower>();
    
    public ICollection<Saved> Saved { get; set; } = new HashSet<Saved>();
}