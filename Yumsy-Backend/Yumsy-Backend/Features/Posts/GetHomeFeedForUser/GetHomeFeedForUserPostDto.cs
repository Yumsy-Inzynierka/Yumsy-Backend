namespace Yumsy_Backend.Features.Posts.GetHomeFeed;

public class GetHomeFeedForUserPostDto
{
    public string PostTitle { get; set; }
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string ImageURL { get; set; }
    public DateTime TimePosted { get; set; }
}