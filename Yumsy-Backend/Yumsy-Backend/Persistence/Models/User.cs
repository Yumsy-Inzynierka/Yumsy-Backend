using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class User
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string? ProfileName { get; set; }

    public string? ProfilePicture { get; set; }

    public string? Bio { get; set; }

    public int FollowersCount { get; set; }

    public int FollowingCount { get; set; }

    public int RecipesCount { get; set; }

    public DateTime RegistrationDate { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

    public virtual ICollection<Post> PostsNavigation { get; set; } = new List<Post>();

    public virtual ICollection<User> UserFolloweds { get; set; } = new List<User>();

    public virtual ICollection<User> UserFollowings { get; set; } = new List<User>();
}
