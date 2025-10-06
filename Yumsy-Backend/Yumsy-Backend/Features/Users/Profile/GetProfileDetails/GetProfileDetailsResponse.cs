namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

public record GetProfileDetailsResponse
{
    public Guid Id { get; init; }
    public string Username { get; init; }
    public string ProfileName { get; init; }
    public int RecipesCount { get; init; }
    public int FollowersCount { get; init; }
    public int RecreationsCount { get; init; }
    public string? Bio { get; init; }
    public string? ProfilePicture { get; init; }

    public List<GetProfilePostsResponse>? ProfilePosts { get; set; }
}

public record GetProfilePostsResponse
{
    public Guid Id { get; init; }
    public string Image { get; init; }
}