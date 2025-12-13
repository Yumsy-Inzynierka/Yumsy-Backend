using Microsoft.EntityFrameworkCore;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

[Keyless]
public class RecommendPostResultDTO
{
    public Guid post_id { get; set; }
    public decimal? score { get; set; }
    public string category { get; set; }
    public string image { get; set; }
}