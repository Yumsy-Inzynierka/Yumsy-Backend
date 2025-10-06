using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.AddPost;

public class AddPostHandler
{
    private readonly SupabaseDbContext _dbContext;
    
    public AddPostHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AddPostResponse> Handle(AddPostRequest addPostRequest, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == addPostRequest.UserId);
        
        if (!userExists)
            throw new KeyNotFoundException("User not found.");
        
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = addPostRequest.Body.Title,
            Description = addPostRequest.Body.Description,
            CookingTime = addPostRequest.Body.CookingTime ?? 0,
            LikesCount = 0,
            CommentsCount = 0,
            SavedCount = 0,
            SharedCount = 0,
            UserId = addPostRequest.UserId,
        };
        
        foreach (var image in addPostRequest.Body.Images)
        {
            post.PostImages.Add(new PostImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = image.Image,
                PostId = post.Id
            });
        }
        
        foreach (var tag in addPostRequest.Body.Tags)
        {
            post.PostTags.Add(new PostTag
            {
                PostId = post.Id,
                TagId = tag.Id
            });
        }
        
        foreach (var ingredient in addPostRequest.Body.Ingredients)
        {
            post.IngredientPosts.Add(new IngredientPost
            {
                PostId = post.Id,
                IngredientId = ingredient.Id,
                Quantity = ingredient.Quantity
            });
        }
        
        foreach (var step in addPostRequest.Body.RecipeSteps)
        {
            post.Steps.Add(new Step
            {
                Id = Guid.NewGuid(),
                Description = step.Description,
                StepNumber = step.StepNumber,
                ImageUrl = step.Image,
                PostId = post.Id
            });
        }
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
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

        return new AddPostResponse
        {
            Id = post.Id,
        };
    }
}