using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("user_follower")]
[PrimaryKey(nameof(FollowingId), nameof(FollowerId))]
public class UserFollower
{
    public Guid FollowingId { get; set; }
    
    [ForeignKey(nameof(FollowingId))]
    public User Following { get; set; }

    public Guid FollowerId { get; set; }
    [ForeignKey(nameof(FollowerId))]
    public User Follower { get; set; }

    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}