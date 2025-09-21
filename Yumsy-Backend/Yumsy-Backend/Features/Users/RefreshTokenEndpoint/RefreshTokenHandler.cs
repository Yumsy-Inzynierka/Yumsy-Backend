using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

public class RefreshTokenHandler
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public RefreshTokenHandler(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request)
    {
        var supabaseUrl = _configuration["Supabase:Url"];
        var supabaseKey = _configuration["Supabase:AnonKey"];

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("apikey", supabaseKey);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", supabaseKey);

        var httpContent = new StringContent(
            JsonSerializer.Serialize(new { refresh_token = request.RefreshToken }),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(
            $"{supabaseUrl}/auth/v1/token?grant_type=refresh_token",
            httpContent
        );

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new ArgumentException($"Cannot refresh token: {content}");

        var session = JsonSerializer.Deserialize<SupabaseTokenResponse>(content);

        return new RefreshTokenResponse
        {
            AccessToken = session.access_token,
            RefreshToken = session.refresh_token
        };
    }
}