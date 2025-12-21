using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("post_image")]
public class PostImage
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Column("image_url")]
    public string ImageUrl { get; set; }
    
    [Column("post_id")]
    public Guid PostId { get; set; }

    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
}