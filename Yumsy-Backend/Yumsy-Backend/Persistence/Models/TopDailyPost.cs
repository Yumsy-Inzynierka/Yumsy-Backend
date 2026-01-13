using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yumsy_Backend.Persistence.Models;

[Table("top_daily_post")]
public class TopDailyPost
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Required]
    [Column("post_id")]
    public Guid PostId { get; set; }
    
    [Required]
    [Column("title")]
    public string Title { get; set; }
    
    [Required]
    [Column("username")]
    public string Username { get; set; }
    
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("image_url")]
    public string ImageUrl { get; set; }
    
    [Required]
    [Column("posted_date")]
    public DateTime PostedDate { get; set; }
    
    [Required]
    [Column("likes_count")]
    public string LikesCount { get; set; }
    
    [Required]
    [Column("comments_count")]
    public string CommentsCount { get; set; }
    
    [Required]
    [Column("date")]
    public DateOnly Date { get; set; }
    
    [Required]
    [Range(1, 6)]
    [Column("rank")]
    public int Rank { get; set; }
    
    [ForeignKey(nameof(PostId))] 
    public Post Post { get; set; }
}