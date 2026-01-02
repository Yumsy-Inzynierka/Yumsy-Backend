using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Yumsy_Backend.Extensions;
using Yumsy_Backend.Features.Ingredients.SearchIngredients;
using Yumsy_Backend.Features.Posts.AddPost;
using Yumsy_Backend.Features.Posts.Comments.AddComment;
using Yumsy_Backend.Features.Posts.Comments.DeleteComment;
using Yumsy_Backend.Features.Posts.Comments.GetPostComments;
using Yumsy_Backend.Features.Posts.DeletePost;
using Yumsy_Backend.Features.Posts.EditPost;
using Yumsy_Backend.Features.Posts.GetExplorePagePosts;
using Yumsy_Backend.Features.Posts.GetPostDetails;
using Yumsy_Backend.Features.Posts.GetHomeFeedForUser;
using Yumsy_Backend.Features.Posts.GetNewPosts;
using Yumsy_Backend.Features.Posts.GetSavedPosts;
using Yumsy_Backend.Features.Posts.GetTopDailyPosts;
using Yumsy_Backend.Features.Posts.Likes.LikeComment;
using Yumsy_Backend.Features.Posts.Likes.LikePost;
using Yumsy_Backend.Features.Posts.Likes.UnlikeComment;
using Yumsy_Backend.Features.Posts.Likes.UnlikePost;
using Yumsy_Backend.Features.Posts.SavePost;
using Yumsy_Backend.Features.Posts.SearchPosts;
using Yumsy_Backend.Features.Posts.UnsavePost;
using Yumsy_Backend.Features.Quiz.GetQuizQuestions;
using Yumsy_Backend.Features.Quiz.GetQuizResult;
using Yumsy_Backend.Features.ShoppingLists.AddShoppingList;
using Yumsy_Backend.Features.ShoppingLists.CreateShoppingList;
using Yumsy_Backend.Features.ShoppingLists.DeleteShoppingList;
using Yumsy_Backend.Features.ShoppingLists.EditShoppingList;
using Yumsy_Backend.Features.ShoppingLists.GetShoppingLists;
using Yumsy_Backend.Features.Tags.GetTags;
using Yumsy_Backend.Features.Tags.GetTopDailyTags;
using Yumsy_Backend.Features.Temporary.DropSeenPost;
using Yumsy_Backend.Features.Users.FollowUser;
using Yumsy_Backend.Features.Users.Login;
using Yumsy_Backend.Features.Users.Profile.GetLikedPosts;
using Yumsy_Backend.Features.Users.Profile.EditProfileDetails;
using Yumsy_Backend.Features.Users.Profile.GetProfileDetails;
using Yumsy_Backend.Features.Users.RefreshTokenEndpoint;
using Yumsy_Backend.Features.Users.Register;
using Yumsy_Backend.Features.Users.SearchUsers;
using Yumsy_Backend.Features.Users.UnfollowUser;
using Yumsy_Backend.Middlewares.ExceptionHandlingMiddleware;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Shared.EventLogger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Dodanie DbContext z EF Core
builder.Services.AddDbContext<SupabaseDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("SupabaseConnection")).UseSnakeCaseNamingConvention());

builder.Services.AddHttpClient();
builder.Services.AddSingleton(configuration);

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
builder.Services.AddScoped<GetTopDailyPostsHandler>();
builder.Services.AddScoped<GetTopDailyTagsHandler>();
builder.Services.AddScoped<GetLikedPostsHandler>();
builder.Services.AddScoped<EditShoppingListHandler>();
builder.Services.AddScoped<AddShoppingListHandler>();
builder.Services.AddScoped<RefreshTokenHandler>();
builder.Services.AddScoped<EditProfileDetailsHandler>();
builder.Services.AddScoped<GetExplorePagePostsHandler>();
builder.Services.AddScoped<EditPostHandler>();
builder.Services.AddScoped<CreateShoppingListHandler>();
builder.Services.AddScoped<GetNewPostsHandler>();
builder.Services.AddScoped<GetSavedPostsHandler>();
builder.Services.AddScoped<GetQuizQuestionsHandler>();
builder.Services.AddScoped<GetQuizResultHandler>();
builder.Services.AddScoped<GetTagsHandler>();
builder.Services.AddScoped<SearchPostsHandler>();
builder.Services.AddScoped<SearchUsersHandler>();
builder.Services.AddScoped<DeletePostHandler>();
//to be removed:
builder.Services.AddScoped<DropSeenPostHandler>();

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
builder.Services.AddScoped<IValidator<GetTopDailyPostsRequest>, GetTopDailyPostsValidator>();
builder.Services.AddScoped<IValidator<GetTopDailyTagsRequest>, GetTopDailyTagsValidator>();
builder.Services.AddScoped<IValidator<GetLikedPostsRequest>, GetLikedPostsValidator>();
builder.Services.AddScoped<IValidator<EditShoppingListRequest>, EditShoppingListValidator>();
builder.Services.AddScoped<IValidator<AddShoppingListRequest>, AddShoppingListValidator>();
builder.Services.AddScoped<IValidator<RefreshTokenRequest>, RefreshTokenValidator>();
builder.Services.AddScoped<IValidator<EditProfileDetailsRequest>, EditProfileDetailsValidator>();
builder.Services.AddScoped<IValidator<GetExplorePagePostsRequest>, GetExplorePagePostsValidator>();
builder.Services.AddScoped<IValidator<EditPostRequest>, EditPostValidator>();
builder.Services.AddScoped<IValidator<CreateShoppingListRequest>, CreateShoppingListValidator>();
builder.Services.AddScoped<IValidator<GetNewPostsRequest>, GetNewPostsValidator>();
builder.Services.AddScoped<IValidator<GetSavedPostsRequest>, GetSavedPostsValidator>();
builder.Services.AddScoped<IValidator<GetQuizQuestionsRequest>, GetQuizQuestionsValidator>();
builder.Services.AddScoped<IValidator<GetQuizResultRequest>, GetQuizResultValidator>();
builder.Services.AddScoped<IValidator<GetTagsRequest>, GetTagsValidator>();
builder.Services.AddScoped<IValidator<SearchPostsRequest>, SearchPostsValidator>();
builder.Services.AddScoped<IValidator<SearchUsersRequest>, SearchUsersValidator>();
builder.Services.AddScoped<IValidator<DeletePostRequest>, DeletePostValidator>();

builder.Services.AddSingleton<IExceptionStatusCodeMapper, ExceptionStatusCodeMapper>();
builder.Services.AddScoped<IAppEventLogger, AppEventLogger>();

// Konfiguracja uwierzytelniania JWT z Supabase
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = $"{configuration["Supabase:Url"]}/auth/v1",

            ValidateAudience = false,
            ValidateLifetime = true,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Supabase:JwtSecret"])
            )
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });

    // Dodanie Bearer token
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Wprowadź token JWT w formacie: Bearer {token}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseGlobalExceptionMiddleware();
app.UseGlobalHttpLoggingMiddleware();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
