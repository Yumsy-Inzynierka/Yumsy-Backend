using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Post
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime PostedDate { get; set; }

    public int LikesCount { get; set; }

    public int CommentsCount { get; set; }

    public int SavedCount { get; set; }

    public int SharedCount { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<IngredientPost> IngredientPosts { get; set; } = new List<IngredientPost>();

    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

    public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();

    public virtual ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
