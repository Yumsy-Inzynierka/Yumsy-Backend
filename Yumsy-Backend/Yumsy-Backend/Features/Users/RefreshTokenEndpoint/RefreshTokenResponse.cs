namespace Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}