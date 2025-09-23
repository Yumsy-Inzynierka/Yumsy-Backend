namespace Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

public class SupabaseTokenResponse
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public long expires_at { get; set; }
    public string refresh_token { get; set; }
    public SupabaseUser user { get; set; }
}

public class SupabaseUser
{
    public string id { get; set; }
    public string aud { get; set; }
    public string role { get; set; }
    public string email { get; set; }
    public DateTime created_at { get; set; }
}