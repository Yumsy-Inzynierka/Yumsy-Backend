using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("user")]
public class User
{
    [Key]
    public Guid Id { get; set; }

    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }
    
    [MaxLength(20)]
    public string Username { get; set; }
    
    [MaxLength(20)]

    public string? ProfileName { get; set; }
    
    public string? ProfilePicture { get; set; }

    [MaxLength(400)]
    public string? Bio { get; set; }

    public int FollowersCount { get; set; }

    public int FollowingCount { get; set; }

    public int RecipesCount { get; set; }

    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    [MaxLength(20)]
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