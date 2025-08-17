namespace Yumsy_Backend.Features.Users.Login;

public record LoginResponse
{
    public Guid UserId { get; init; }
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
    public string Role { get; init; }
    public string Email { get; init; }
    public string UserName { get; init; }
    
}