using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;
using Yumsy_Backend.Shared.EventLogger;

namespace Yumsy_Backend.Features.Posts.AddPost;

public class AddPostHandler
{
    private readonly SupabaseDbContext _dbContext;
    private readonly IAppEventLogger _appEventLogger;
    
    public AddPostHandler(SupabaseDbContext dbContext, IAppEventLogger appEventLogger)
    {
        _dbContext = dbContext;
        _appEventLogger = appEventLogger;
    }

    public async Task<AddPostResponse> Handle(AddPostRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == request.UserId);

        if (!userExists)
            throw new KeyNotFoundException("User not found.");

        var post = new Post
        {
            Title = request.Body.Title,
            Description = request.Body.Description,
            CookingTime = request.Body.CookingTime ?? 0,
            UserId = request.UserId,

            PostImages = request.Body.Images
                .Select(img => new PostImage
                {
                    ImageUrl = img.Image
                })
                .ToList(),

            Steps = request.Body.RecipeSteps
                .Select(rs => new Step
                {
                    StepNumber = rs.StepNumber,
                    Description = rs.Description,
                    ImageUrl = rs.Image
                })
                .ToList(),

            PostTags = request.Body.Tags
                .Select(t => new PostTag
                {
                    TagId = t.Id
                })
                .ToList(),
            
            IngredientPosts = request.Body.Ingredients
                .Select(i => new IngredientPost
                {
                    IngredientId = i.Id,
                    Quantity = i.Quantity
                }).ToList()
        };
        
        await using var transaction =
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _dbContext.Posts.AddAsync(post, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
        
        await _appEventLogger.LogAsync(
            action: "create_post",
            userId: request.UserId,
            entityId: post.Id
        );

        return new AddPostResponse { Id = post.Id };
    }
}