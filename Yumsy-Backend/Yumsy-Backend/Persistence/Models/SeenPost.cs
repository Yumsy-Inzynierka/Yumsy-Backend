using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("seen_post")]
[PrimaryKey(nameof(UserId), nameof(PostId))]
public class SeenPost
{
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    
    [ForeignKey(nameof(PostId))]
    public Post Post { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
}