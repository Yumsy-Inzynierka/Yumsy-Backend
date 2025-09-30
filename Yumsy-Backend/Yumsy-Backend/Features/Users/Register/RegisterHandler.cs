using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Users.Register;

public class RegisterHandler
{
    private readonly IConfiguration _config;
    private readonly SupabaseDbContext _dbContext;

    public RegisterHandler(IConfiguration config, SupabaseDbContext dbContext)
    {
        _config = config;
        _dbContext = dbContext;
    }

    public async Task Handle(RegisterRequest request)
    {
        var usernameExist = await _dbContext.Users
            .AnyAsync(u => u.Username == request.Username);
        
        if (usernameExist)
            throw new InvalidOperationException($"User name: {request.Username} is already registered.");
        
        var profileNameExist = await _dbContext.Users
            .AnyAsync(u => u.ProfileName == request.Username);
        
        if (profileNameExist)
            throw new InvalidOperationException($"User name: {request.Username} is already registered as profile name.");

        var emailExist = await _dbContext.Users
            .AnyAsync(u => u.Email == request.Email);
        
        if (emailExist)
            throw new InvalidOperationException($"Email: {request.Email} is already registered.");
        
        var client = new Supabase.Client(
            _config["Supabase:Url"],
            _config["Supabase:ServiceKey"],
            new Supabase.SupabaseOptions { AutoConnectRealtime = false }
        );
        await client.InitializeAsync();

        var signUpOptions = new Supabase.Gotrue.SignUpOptions
        {
            Data = new Dictionary<string, object> { { "username", request.Username } }
        };

        var signUpResult = await client.Auth.SignUp(request.Email, request.Password, signUpOptions);

        if (signUpResult.User == null)
            throw new ArgumentException("Failed to register user in Supabase Auth");

        var userId = Guid.Parse(signUpResult.User.Id);
        var user = new User
        {
            Id = userId,
            Email = request.Email,
            Username = request.Username,
            ProfileName = request.Username,
            Role = "user",
            FollowersCount = 0,
            FollowingCount = 0,
            RecipesCount = 0
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}
