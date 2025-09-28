using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("comment")]
public class Comment
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CommentedDate { get; set; } = DateTime.UtcNow;
    public int LikesCount { get; set; }

    [MaxLength(400)]
    public string Content { get; set; }

    public Guid UserId { get; set; }

    public Guid PostId { get; set; }

    public Guid? ParentCommentId { get; set; }

    [ForeignKey(nameof(ParentCommentId))]
    public Comment? ParentComment { get; set; }

    public ICollection<CommentLike> CommentLikes { get; set; } = new HashSet<CommentLike>();

    public ICollection<Comment> ChildComments { get; set; } = new HashSet<Comment>();

    [ForeignKey(nameof(UserId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(PostId))]
    public User User { get; set; }
}