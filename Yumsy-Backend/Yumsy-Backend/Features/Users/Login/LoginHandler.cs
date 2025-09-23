using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Login;

public class LoginHandler
{
    private readonly IConfiguration _config;
    private readonly SupabaseDbContext _dbContext;

    public LoginHandler(IConfiguration config, SupabaseDbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        var client = new Supabase.Client(
            _config["Supabase:Url"],
            _config["Supabase:ServiceKey"],
            new Supabase.SupabaseOptions
            {
                AutoConnectRealtime = false
            }
        );

        await client.InitializeAsync();

        var signInResult = await client.Auth.SignIn(
            email: request.Email,
            password: request.Password
        );

        if (signInResult?.User == null)
            throw new ArgumentException("Invalid email or password");

        if (!Guid.TryParse(signInResult.User.Id, out Guid userId))
            throw new FormatException("Invalid user ID format from auth provider");

        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new KeyNotFoundException("User not found in database");

        return new LoginResponse
        {
            UserId = user.Id,
            AccessToken = signInResult.AccessToken,
            RefreshToken = signInResult.RefreshToken,
            Email = user.Email,
            UserName = user.Username,
            Role = user.Role,
        };
    }
}