using Microsoft.EntityFrameworkCore;
using Supabase.Postgrest.Exceptions;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Modele;

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

    public async Task<RegisterResponse> Handle(RegisterRequest request)
    {
        try
        {
            await _supabaseClient.InitializeAsync();

            // 1. Rejestracja użytkownika w Supabase Auth
            var signUpOptions = new Supabase.Gotrue.SignUpOptions
            {
                Data = new Dictionary<string, object>
                {
                    { "username", request.Username }
                }
            };

            var signUpResult = await _supabaseClient.Auth.SignUp(request.Email, request.Password, signUpOptions);

            if (signUpResult.User == null)
            {
                return new RegisterResponse()
                {
                    Success = false,
                    Message = "Failed to register user in Supabase Auth",
                    AccessToken = null
                };
            }

            // 2. Dodanie usera do bazy danych przez EF
            var userId = signUpResult.User.Id;
            var registrationDate = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var user = new User
            {
                Id = Guid.Parse(userId),
                Email = request.Email,
                Username = request.Username,
                Role = "user",
                FollowersCount = 0,
                FollowingCount = 0,
                RecipesCount = 0,
                RegistrationDate = registrationDate
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return new RegisterResponse()
            {
                Success = true,
                Message = "huuura",
                AccessToken = "adawd"
            };
        }
        catch (PostgrestException ex)
        {
            // Błąd Supabase Postgrest
            return new RegisterResponse()
            {
                Success = false,
                Message = $"Supabase error: {ex.Message}",
                AccessToken = null
            };
        }
        catch (DbUpdateException ex)
        {
            // Błąd EF Core
            return new RegisterResponse()
            {
                Success = false,
                Message = $"Database error: {ex.Message}",
                AccessToken = null
            };
        }
        catch (Exception ex)
        {
            // Inne błędy
            return new RegisterResponse()
            {
                Success = false,
                Message = $"Unexpected error: {ex.Message}",
                AccessToken = null
            };
        }
    }
}
