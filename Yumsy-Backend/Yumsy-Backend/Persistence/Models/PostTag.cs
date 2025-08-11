

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("post_tag")]
[PrimaryKey(nameof(PostId), nameof(TagId))]
public class PostTag
{
    public Guid PostId { get; set; }
    public Guid TagId { get; set; }
    
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
}