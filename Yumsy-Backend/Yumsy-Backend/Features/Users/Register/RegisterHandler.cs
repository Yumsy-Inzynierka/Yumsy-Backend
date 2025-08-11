using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Users.Register;

public class RegisterHandler
{
    private readonly Supabase.Client _supabaseClient;
    private readonly SupabaseDbContext _dbContext;

    public RegisterHandler(Supabase.Client supabaseClient, SupabaseDbContext dbContext)
    {
        _supabaseClient = supabaseClient;
        _dbContext = dbContext;
    }

    public async Task Handle(RegisterRequest request)
    {
        await _supabaseClient.InitializeAsync();

        var signUpOptions = new Supabase.Gotrue.SignUpOptions
        {
            Data = new Dictionary<string, object> { { "username", request.Username } }
        };

        var signUpResult = await _supabaseClient.Auth.SignUp(request.Email, request.Password, signUpOptions);

        if (signUpResult.User == null)
            throw new ArgumentException("Failed to register user in Supabase Auth");
        
        var userId = Guid.Parse(signUpResult.User.Id);
        
        var user = new User
        {
            Id = userId,
            Email = request.Email,
            Username = request.Username,
            Role = "user",
            FollowersCount = 0,
            FollowingCount = 0,
            RecipesCount = 0,
        };
        
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}