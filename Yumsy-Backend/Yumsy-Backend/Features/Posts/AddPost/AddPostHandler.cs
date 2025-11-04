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

    public async Task<AddPostResponse> Handle(AddPostRequest request, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(u => u.Id == request.UserId);
        
        if (!userExists)
            throw new KeyNotFoundException("User not found.");
        
        // nie jest to obowiązkowe bo my bierzemy z fronta z listy tagi i ingredients ale może być bo dobra praktyka, oby nie zwalniało (do przemyślenia)
        var tagIds = request.Body.Tags.Select(t => t.Id).ToList();
        var existingTags = await _dbContext.Tags
            .Where(t => tagIds.Contains(t.Id))
            .Select(t => t.Id)
            .ToListAsync(cancellationToken);
        
        if (existingTags.Count != tagIds.Count)
            throw new KeyNotFoundException("One or more tags not found.");
        
        var ingredientIds = request.Body.Ingredients.Select(i => i.Id).ToList();
        var existingIngredients = await _dbContext.Ingredients
            .Where(i => ingredientIds.Contains(i.Id))
            .Select(i => i.Id)
            .ToListAsync(cancellationToken);
        
        if (existingIngredients.Count != ingredientIds.Count)
            throw new KeyNotFoundException("One or more ingredients not found.");
        
        
        var post = new Post
        {
            Id = Guid.NewGuid(),
            Title = request.Body.Title,
            Description = request.Body.Description,
            CookingTime = request.Body.CookingTime ?? 0,
            LikesCount = 0,
            CommentsCount = 0,
            SavedCount = 0,
            SharedCount = 0,
            UserId = request.UserId,
        };
        
        foreach (var image in request.Body.Images)
        {
            post.PostImages.Add(new PostImage
            {
                Id = Guid.NewGuid(),
                ImageUrl = image.Image,
                PostId = post.Id
            });
        }
        
        foreach (var tag in request.Body.Tags)
        {
            post.PostTags.Add(new PostTag
            {
                PostId = post.Id,
                TagId = tag.Id
            });
        }
        
        foreach (var ingredient in request.Body.Ingredients)
        {
            post.IngredientPosts.Add(new IngredientPost
            {
                PostId = post.Id,
                IngredientId = ingredient.Id,
                Quantity = ingredient.Quantity
            });
        }
        
        foreach (var step in request.Body.RecipeSteps)
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