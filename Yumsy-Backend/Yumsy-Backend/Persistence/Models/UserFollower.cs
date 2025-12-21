using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Persistence.Models;

[Table("user_follower")]
[PrimaryKey(nameof(FollowingId), nameof(FollowerId))]
public class UserFollower
{
    [Column("following_id")]
    public Guid FollowingId { get; set; }
    //osoba która jest obserwowana 
    [ForeignKey(nameof(FollowingId))]
    public User Following { get; set; }
    
    //osoba która obserwuje
    [Column("follower_id")]
    public Guid FollowerId { get; set; }
    [ForeignKey(nameof(FollowerId))]
    public User Follower { get; set; }

    [Column("followed_at")]
    public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
}