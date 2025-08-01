using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Yumsy_Backend.Features.Users.Login;
using Yumsy_Backend.Features.Users.Register;
using Yumsy_Backend.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Dodanie DbContext z EF Core
builder.Services.AddDbContext<SupabaseDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("SupabaseConnection")));

// Konfiguracja Supabase Clienta
var supabaseUrl = configuration["Supabase:Url"];
var supabaseJWKS = $"{supabaseUrl}/auth/v1/keys";

builder.Services.AddHttpClient();
builder.Services.AddSingleton(configuration);

builder.Services.AddSingleton(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var url = config["Supabase:Url"];
    var key = config["Supabase:ServiceKey"];

    return new Supabase.Client(url, key, new Supabase.SupabaseOptions
    {
        AutoConnectRealtime = false,
        AutoRefreshToken = true
    });
});

// Rejestracja handlerów i walidatorów
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<LoginHandler>();

builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();

// Konfiguracja uwierzytelniania JWT z Supabase
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.MetadataAddress = supabaseJWKS;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = supabaseUrl,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
