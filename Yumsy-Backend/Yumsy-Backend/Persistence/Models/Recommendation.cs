using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("recommendation")]
[PrimaryKey(nameof(TagId), nameof(UserId))]
public class Recommendation
{
    
    public Guid TagId { get; set; }
    public Guid UserId { get; set; }
    
    [ForeignKey(nameof(TagId))]
    public Tag Tag { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public int Score { get; set; }
}