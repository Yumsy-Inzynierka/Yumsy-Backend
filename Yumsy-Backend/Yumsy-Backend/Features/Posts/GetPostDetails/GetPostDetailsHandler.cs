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
            .ThenInclude(s => s.Photo)
            .FirstOrDefaultAsync(p => p.Id == detailsRequest.PostId);

        if (post == null)
            throw new KeyNotFoundException("Post does not exist");

        var ingredients = await _dbContext.IngredientPosts
            .Where(ip => ip.PostId == detailsRequest.PostId)
            .Select(ip => new
            {
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

        decimal totalCalories = 0;
        decimal totalFats = 0;
        decimal totalCarbs = 0;
        decimal? totalFiber = 0;
        decimal? totalSugars = 0;
        decimal totalProtein = 0;
        decimal? totalSalt = 0;

        var ingredientResponses = new List<GetPostIngredientResponse>();

        bool isTotalFiberNullFlag = false;
        bool isTotalSugarsNullFlag = false;
        bool isTotalSaltsNullFlag = false;

        foreach (var ingredient in ingredients)
        {
            decimal multiplier = ingredient.Quantity / 100m;

            totalCalories += ingredient.EnergyKcal100g * multiplier;
            totalFats += ingredient.Fat100g * multiplier;
            totalCarbs += ingredient.Carbohydrates100g * multiplier;
            totalProtein += ingredient.Proteins100g * multiplier;
            if (totalFiber == null)
            {
                isTotalFiberNullFlag = true;
            }
            else
            {
                totalFiber += ingredient.Fiber100g * multiplier;
            }
            
            if (totalFiber == null)
            {
                isTotalSugarsNullFlag = true;
            }
            else
            {
                totalSugars += ingredient.Sugars100g * multiplier;
            }
            
            if (totalFiber == null)
            {
                isTotalSaltsNullFlag = true;
            }
            else
            {
                totalSalt += ingredient.Salt100g * multiplier;
            }

            ingredientResponses.Add(new GetPostIngredientResponse
            {
                Quantity = ingredient.Quantity,
                Name = ingredient.Name
            });
        }

        var recipeSteps = post.Steps
            .OrderBy(rs => rs.StepNumber)
            .Select(rs => new GetPostRecipeStepResponse
            {
                StepNumber = rs.StepNumber,
                Description = rs.Description,
                PhotoUrl = rs.Photo.ImageUrl
            })
            .ToList();

        return new GetPostDetailsResponse
        {
            PostId = post.Id,
            Title = post.Title,
            CookingTime = post.CookingTime,
            Description = post.Description,
            Ingredients = ingredientResponses,
            Calories = totalCalories,
            Fats = totalFats,
            Carbohydrates = totalCarbs,
            Fiber = isTotalFiberNullFlag ? null : totalFiber,
            Sugars = isTotalSugarsNullFlag ? null : totalSugars,
            Protein = totalProtein,
            Salt = isTotalSaltsNullFlag ? null : totalSalt,
            RecipeSteps = recipeSteps
        };
    }
}