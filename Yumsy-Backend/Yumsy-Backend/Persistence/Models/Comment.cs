using System;
using System.Collections.Generic;

namespace Yumsy_Backend.Persistence.Modele;

public partial class Comment
{
    public Guid Id { get; set; }

    public DateTime CommentedDate { get; set; }

    public string Content { get; set; } = null!;

    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public Guid? ChildComment { get; set; }

    public virtual Comment? ChildCommentNavigation { get; set; }

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual ICollection<Comment> InverseChildCommentNavigation { get; set; } = new List<Comment>();

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
