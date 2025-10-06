namespace Yumsy_Backend.Features.Users.Profile.GetLikedPosts;
public record GetLikedPostsResponse
{
    public List<GetLikedPostResponse> Posts { get; set; }
}


public record GetLikedPostResponse
{
    public Guid Id { get; set; }
    public string Image { get; set; }
}