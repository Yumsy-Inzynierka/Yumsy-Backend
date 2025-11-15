using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;
using Yumsy_Backend.Persistence.Models;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetPostDetailsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPostDetailsResponse> Handle(GetPostDetailsRequest request, CancellationToken cancellationToken)
    {
        var post = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Steps)
            .Include(p => p.CreatedBy)
            .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.PostImages)
            .FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);

        if (post == null)
            throw new KeyNotFoundException($"Post with ID: {request.PostId} does not exist.");

        var ingredients = await _dbContext.IngredientPosts
            .AsNoTracking()
            .Where(ip => ip.PostId == request.PostId)
            .Include(ip => ip.Ingredient)
            .ToListAsync(cancellationToken);

        var ingredientResponses = new List<GetPostIngredientResponse>();

        decimal totalCalories = 0m;
        decimal totalFats = 0m;
        decimal totalCarbs = 0m;
        decimal totalProtein = 0m;

        decimal? totalFiber = 0m;
        decimal? totalSugars = 0m;
        decimal? totalSalt = 0m;

        bool fiberNull = false;
        bool sugarsNull = false;
        bool saltNull = false;

        foreach (var ip in ingredients)
        {
            var multiplier = ip.Quantity / 100m;

            totalCalories += ip.Ingredient.EnergyKcal100g * multiplier;
            totalFats += ip.Ingredient.Fat100g * multiplier;
            totalCarbs += ip.Ingredient.Carbohydrates100g * multiplier;
            totalProtein += ip.Ingredient.Proteins100g * multiplier;

            if (ip.Ingredient.Fiber100g.HasValue)
                totalFiber += ip.Ingredient.Fiber100g.Value * multiplier;
            else fiberNull = true;

            if (ip.Ingredient.Sugars100g.HasValue)
                totalSugars += ip.Ingredient.Sugars100g.Value * multiplier;
            else sugarsNull = true;

            if (ip.Ingredient.Salt100g.HasValue)
                totalSalt += ip.Ingredient.Salt100g.Value * multiplier;
            else saltNull = true;

            ingredientResponses.Add(new GetPostIngredientResponse
            {
                Id = ip.IngredientId,
                Quantity = ip.Quantity,
                Name = ip.Ingredient.Name
            });
        }

        var recipeSteps = post.Steps
            .OrderBy(s => s.StepNumber)
            .Select(s => new GetPostRecipeStepResponse
            {
                Id = s.Id,
                StepNumber = s.StepNumber,
                Description = s.Description,
                Image = s.ImageUrl
            })
            .ToList();

        var tags = post.PostTags
            .Select(pt => new GetPostTagResponse
            {
                Id = pt.Tag.Id,
                Name = pt.Tag.Name
            })
            .ToList();

        var images = post.PostImages.Select(pi => pi.ImageUrl).ToList();

        var isLiked = await _dbContext.Likes
            .AsNoTracking()
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);

        var isSaved = await _dbContext.Saved
            .AsNoTracking()
            .AnyAsync(l => l.PostId == request.PostId && l.UserId == request.UserId, cancellationToken);
        
        return new GetPostDetailsResponse
        {
            Id = post.Id,
            UserId = post.UserId,
            Username = post.CreatedBy.Username,
            IsLiked = isLiked,
            IsSaved = isSaved,
            Title = post.Title,
            Description = post.Description,
            CookingTime = post.CookingTime,
            LikesCount = post.LikesCount,
            CommentsCount = post.CommentsCount,
            Tags = tags,
            Images = images,
            Ingredients = ingredientResponses,
            Nutrition = new GetPostNutritionResponse
            {
                Calories = totalCalories,
                Fats = totalFats,
                TotalCarbohydrates = totalCarbs,
                Protein = totalProtein,
                Fiber = fiberNull ? null : totalFiber,
                Sugars = sugarsNull ? null : totalSugars,
                Sodium = saltNull ? null : totalSalt
            },
            RecipeSteps = recipeSteps
        };
    }
}
