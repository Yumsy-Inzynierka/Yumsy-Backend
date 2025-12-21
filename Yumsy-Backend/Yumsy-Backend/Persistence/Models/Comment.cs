using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("comment")]
public class Comment
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("commented_date")]
    public DateTime CommentedDate { get; set; } = DateTime.UtcNow;
    
    [Column("likes_count")]
    public int LikesCount { get; set; }

    [MaxLength(400)]
    [Column("content")]
    public string Content { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("post_id")]
    public Guid PostId { get; set; }

    [Column("parent_comment_id")]
    public Guid? ParentCommentId { get; set; }

    [ForeignKey(nameof(ParentCommentId))]
    public Comment? ParentComment { get; set; }

    public ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();

    public ICollection<Comment> ChildComments { get; set; } = new HashSet<Comment>();

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}