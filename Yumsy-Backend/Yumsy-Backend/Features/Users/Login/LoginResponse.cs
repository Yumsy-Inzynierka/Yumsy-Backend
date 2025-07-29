namespace Yumsy_Backend.Features.Users.Login;

public class LoginResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string Message { get; set; }
    public string? Role { get; set; }
    public bool Success { get; set; }
    public string Email { get; set; }
    public string ProfileName { get; set; }
    
}