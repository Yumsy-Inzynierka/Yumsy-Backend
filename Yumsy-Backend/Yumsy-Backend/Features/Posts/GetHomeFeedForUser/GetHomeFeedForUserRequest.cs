namespace Yumsy_Backend.Features.Posts.GetHomeFeed;

public class GetHomeFeedForUserRequest
{
    public string UserId { get; set; } = string.Empty;
    //dodac kursor zeby nie powtarzaly sie posty
}