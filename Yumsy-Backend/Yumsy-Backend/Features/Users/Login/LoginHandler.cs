
using User = Yumsy_Backend.Persistence.Models.User;

namespace Yumsy_Backend.Features.Users.Login;

public class LoginHandler
{
    private readonly Supabase.Client _client;

    public LoginHandler(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        await _client.InitializeAsync();

        // 1. Próba zalogowania
        var signInResult = await _client.Auth.SignIn(
            email: request.Email,
            password: request.Password
        );

        if (signInResult.User == null)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        var userId = signInResult.User.Id;

        // 2. Pobranie użytkownika z tabeli (np. "users")
        var userQuery = await _client.From<User>()
            .Where(u => u.Id == Guid.Parse(userId))
            .Get();

        var user = userQuery.Models.FirstOrDefault();

        if (user == null)
        {
            return new LoginResponse
            {
                Success = false,
                Message = "User not found"
            };
        }

        // 3. Zwróć odpowiedź
        return new LoginResponse
        {
            Success = true,
            Message = "Login successful",
            AccessToken = signInResult.AccessToken,
            RefreshToken = signInResult.RefreshToken,
            Email = user.Email,
            ProfileName = user.Username,
            Role = user.Role
        };
    }
}