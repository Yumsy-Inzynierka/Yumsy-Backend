using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Yumsy_Backend.Features.Comments.AddComment;
using Yumsy_Backend.Features.Comments.DeleteComment;
using Yumsy_Backend.Features.Ingredients.SearchIngredient;
using Yumsy_Backend.Features.Posts.AddPost;
using Yumsy_Backend.Features.Posts.Comments.GetPostComments;
using Yumsy_Backend.Features.Posts.GetPostDetails;
using Yumsy_Backend.Features.Posts.GetHomeFeed;
using Yumsy_Backend.Features.Posts.GetTopDailyPostsEndpoint;
using Yumsy_Backend.Features.Users.GetShoppingLists;
using Yumsy_Backend.Features.Posts.LikePost;
using Yumsy_Backend.Features.Posts.Likes.LikeComment;
using Yumsy_Backend.Features.Posts.Likes.UnlikeComment;
using Yumsy_Backend.Features.Posts.SavePost;
using Yumsy_Backend.Features.Posts.UnlikePost;
using Yumsy_Backend.Features.Posts.UnsavePost;
using Yumsy_Backend.Features.ShoppingLists.AddShoppingList;
using Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;
using Yumsy_Backend.Features.ShoppingLists.EditShoppingList;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;
using Yumsy_Backend.Features.Users.FollowUser;
using Yumsy_Backend.Features.Users.Login;
using Yumsy_Backend.Features.Users.Profile.GetLikedPosts;
using Yumsy_Backend.Features.Users.Profile.CreateProfile;
using Yumsy_Backend.Features.Users.Profile.GetProfileDetails;
using Yumsy_Backend.Features.Users.Register;
using Yumsy_Backend.Features.Users.UnfollowUser;
using Yumsy_Backend.Persistence.DbContext;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Dodanie DbContext z EF Core
builder.Services.AddDbContext<SupabaseDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("SupabaseConnection")));

// Konfiguracja Supabase Clienta
var supabaseUrl = configuration["Supabase:Url"];
var supabaseJWKS = $"{supabaseUrl}/auth/v1/.well-known/openid-configuration";

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
builder.Services.AddScoped<GetPostDetailsHandler>();
builder.Services.AddScoped<GetShoppingListsHandler>();
builder.Services.AddScoped<SearchIngredientsHandler>();
builder.Services.AddScoped<GetHomeFeedForUserHandler>();
builder.Services.AddScoped<DeleteShoppingListHandler>();
builder.Services.AddScoped<GetProfileDetailsHandler>();
builder.Services.AddScoped<LikePostHandler>();
builder.Services.AddScoped<UnlikePostHandler>();
builder.Services.AddScoped<AddCommentHandler>();
builder.Services.AddScoped<DeleteCommentHandler>();
builder.Services.AddScoped<GetPostCommentsHandler>();
builder.Services.AddScoped<AddPostHandler>();
builder.Services.AddScoped<SavePostHandler>();
builder.Services.AddScoped<UnsavePostHandler>();
builder.Services.AddScoped<FollowUserHandler>();
builder.Services.AddScoped<UnfollowUserHandler>();
builder.Services.AddScoped<LikeCommentHandler>();
builder.Services.AddScoped<UnlikeCommentHandler>();
builder.Services.AddScoped<AddProfileDetailsHandler>();
builder.Services.AddScoped<GetTopDailyPostsHandler>();
builder.Services.AddScoped<GetTopDailyTagsHandler>();
builder.Services.AddScoped<GetLikedPostsHandler>();
builder.Services.AddScoped<EditShoppingListHandler>();
builder.Services.AddScoped<AddShoppingListHandler>();


builder.Services.AddScoped<IValidator<RegisterRequest>, RegisterValidator>();
builder.Services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
builder.Services.AddScoped<IValidator<GetPostDetailsRequest>, GetPostDetailsValidator>();
builder.Services.AddScoped<IValidator<SearchIngredientsRequest>, SearchIngredientsValidator>();
builder.Services.AddScoped<IValidator<GetHomeFeedForUserRequest>, GetHomeFeedForUserValidator>();
builder.Services.AddScoped<IValidator<DeleteShoppingListRequest>, DeleteShoppingListValidator>();
builder.Services.AddScoped<IValidator<GetPostDetailsRequest>, GetPostDetailsValidator>();
builder.Services.AddScoped<IValidator<GetShoppingListsRequest>, GetShoppingListsValidator>();
builder.Services.AddScoped<IValidator<GetProfileDetailsRequest>, GetProfileDetailsValidator>();
builder.Services.AddScoped<IValidator<LikePostRequest>, LikePostValidator>();
builder.Services.AddScoped<IValidator<UnlikePostRequest>, UnlikePostValidator>();
builder.Services.AddScoped<IValidator<AddCommentRequest>, AddCommentValidator>();
builder.Services.AddScoped<IValidator<DeleteCommentRequest>, DeleteCommentValidator>();
builder.Services.AddScoped<IValidator<GetPostCommentsRequest>, GetPostCommentsValidator>();
builder.Services.AddScoped<IValidator<AddPostRequest>, AddPostValidator>();
builder.Services.AddScoped<IValidator<SavePostRequest>, SavePostValidator>();
builder.Services.AddScoped<IValidator<UnsavePostRequest>, UnsavePostValidator>();
builder.Services.AddScoped<IValidator<FollowUserRequest>, FollowUserValidator>();
builder.Services.AddScoped<IValidator<UnfollowUserRequest>, UnfollowUserValidator>();
builder.Services.AddScoped<IValidator<LikeCommentRequest>, LikeCommentValidator>();
builder.Services.AddScoped<IValidator<UnlikeCommentRequest>, UnlikeCommentValidator>();
builder.Services.AddScoped<IValidator<AddProfileDetailsRequest>, AddProfileDetailsValidator>();
builder.Services.AddScoped<IValidator<GetTopDailyPostsRequest>, GetTopDailyPostsValidator>();
builder.Services.AddScoped<IValidator<GetTopDailyTagsRequest>, GetTopDailyTagsValidator>();
builder.Services.AddScoped<IValidator<GetLikedPostsRequest>, GetLikedPostsValidator>();
builder.Services.AddScoped<IValidator<EditShoppingListRequest>, EditShoppingListValidator>();
builder.Services.AddScoped<IValidator<AddShoppingListRequest>, AddShoppingListValidator>();


builder.Services.AddScoped<GetHomeFeedForUserHandler>();
builder.Services.AddScoped<GetHomeFeedForUserValidator>();

// Konfiguracja uwierzytelniania JWT z Supabase
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"{supabaseUrl}/auth/v1";
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{supabaseUrl}/auth/v1",
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

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Yumsy API V1");
        c.RoutePrefix = "swagger";
    });
}

app.UseGlobalExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
