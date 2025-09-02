using Microsoft.EntityFrameworkCore;
using Yumsy_Backend.Persistence.DbContext;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsHandler
{
    private readonly SupabaseDbContext _dbContext;

    public GetPostDetailsHandler(SupabaseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetPostDetailsResponse> Handle(GetPostDetailsRequest detailsRequest)
    {
        var post = await _dbContext.Posts
            .AsNoTracking()
            .Include(p => p.Steps)
            .Include(p => p.CreatedBy)
            .Include(p => p.PostTags).ThenInclude(pt => pt.Tag)
            .Include(p => p.PostImages)
            .FirstOrDefaultAsync(p => p.Id == detailsRequest.PostId);

        if (post is null)
            throw new KeyNotFoundException("Post does not exist");

        var ingredients = await _dbContext.IngredientPosts
            .Where(ip => ip.PostId == detailsRequest.PostId)
            .Select(ip => new
            {
                ip.IngredientId,
                ip.Quantity,
                ip.Ingredient.Name,
                ip.Ingredient.EnergyKcal100g,
                ip.Ingredient.Fat100g,
                ip.Ingredient.Carbohydrates100g,
                ip.Ingredient.Fiber100g,
                ip.Ingredient.Sugars100g,
                ip.Ingredient.Proteins100g,
                ip.Ingredient.Salt100g
            })
            .ToListAsync();

        decimal? totalCalories = 0,
                 totalFats = 0,
                 totalCarbs = 0,
                 totalFiber = 0,
                 totalSugars = 0,
                 totalProtein = 0,
                 totalSalt = 0;

        bool caloriesNull = false,
             fatsNull = false,
             carbsNull = false,
             fiberNull = false,
             sugarsNull = false,
             proteinNull = false,
             saltNull = false;

        var ingredientResponses = new List<GetPostIngredientResponse>();

        foreach (var ingredient in ingredients)
        {
            var multiplier = ingredient.Quantity / 100m;

            void AddNutrition(ref decimal? total, ref bool nullFlag, decimal? valuePer100g)
            {
                if (valuePer100g is null)
                {
                    nullFlag = true;
                }
                else
                {
                    total += valuePer100g * multiplier;
                }
            }

            AddNutrition(ref totalCalories, ref caloriesNull, ingredient.EnergyKcal100g);
            AddNutrition(ref totalFats, ref fatsNull, ingredient.Fat100g);
            AddNutrition(ref totalCarbs, ref carbsNull, ingredient.Carbohydrates100g);
            AddNutrition(ref totalProtein, ref proteinNull, ingredient.Proteins100g);
            AddNutrition(ref totalFiber, ref fiberNull, ingredient.Fiber100g);
            AddNutrition(ref totalSugars, ref sugarsNull, ingredient.Sugars100g);
            AddNutrition(ref totalSalt, ref saltNull, ingredient.Salt100g);

            ingredientResponses.Add(new GetPostIngredientResponse
            {
                Id = ingredient.IngredientId,
                Quantity = ingredient.Quantity,
                Name = ingredient.Name
            });
        }

        var recipeSteps = post.Steps
            .OrderBy(rs => rs.StepNumber)
            .Select(rs => new GetPostRecipeStepResponse
            {
                Id = rs.Id,
                StepNumber = rs.StepNumber,
                Description = rs.Description,
                ImageUrl = rs.ImageUrl
            })
            .ToList();

        var postTags = post.PostTags
            .Select(pt => new GetPostTagResponse
            {
                Id = pt.Tag.Id,
                Name = pt.Tag.Name
            })
            .ToList();

        var images = post.PostImages
            .Select(p => p.ImageUrl)
            .ToList();

        return new GetPostDetailsResponse
        {
            Id = post.Id,
            Title = post.Title,
            CookingTime = post.CookingTime,
            Description = post.Description,
            Tags = postTags,
            ImagesUrls = images,
            Ingredients = ingredientResponses,
            Nutrition = new GetPostNutritionResponse
            {
                Calories = caloriesNull ? null : totalCalories,
                Fats = fatsNull ? null : totalFats,
                TotalCarbohydrates = carbsNull ? null : totalCarbs,
                Fiber = fiberNull ? null : totalFiber,
                Sugars = sugarsNull ? null : totalSugars,
                Protein = proteinNull ? null : totalProtein,
                Sodium = saltNull ? null : totalSalt
            },
            RecipeSteps = recipeSteps
        };
    }
}
