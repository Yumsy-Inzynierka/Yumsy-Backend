namespace Yumsy_Backend.Features.Users.Register;

public class RegisterResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string AccessToken { get; set; }
}