namespace Yumsy_Backend.Features.Users.SearchUsers;

public record SearchUsersResponse
{
    public List<SearchUserResponse> Users { get; set; }
    public int Page { get; set; }
    public bool HasMore { get; set; }
}

public record SearchUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string ProfileName { get; set; }
    public string ProfilePictureUrl { get; set; }
}