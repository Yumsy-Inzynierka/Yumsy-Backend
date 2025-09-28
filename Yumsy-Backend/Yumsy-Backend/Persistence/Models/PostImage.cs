using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("post_image")]
public class PostImage
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ImageUrl { get; set; }
    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}