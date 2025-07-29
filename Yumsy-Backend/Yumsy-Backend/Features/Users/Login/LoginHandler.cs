using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Users.Login;

public class LoginHandler
{
    private readonly Supabase.Client _supabaseClient;
    private readonly SupabaseDbContext _dbContext;

    public LoginHandler(Supabase.Client supabaseClient, SupabaseDbContext dbContext)
    {
        _supabaseClient = supabaseClient;
        _dbContext = dbContext;
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        await _supabaseClient.InitializeAsync(); 

        var signInResult = await _supabaseClient.Auth.SignIn(
            email: request.Email,
            password: request.Password
        );

        if (signInResult.User == null)
            throw new ArgumentException("Invalid email or password");

        if (!Guid.TryParse(signInResult.User.Id, out Guid userId))
            throw new ArgumentException("Invalid user ID format from auth provider");

        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            throw new ArgumentException("User not found in database");

        return new LoginResponse
        {
            AccessToken = signInResult.AccessToken,
            RefreshToken = signInResult.RefreshToken,
            Email = user.Email,
            UserName = user.Username,
            Role = user.Role
        };
    }
}