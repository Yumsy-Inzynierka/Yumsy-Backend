namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public record GetPostDetailsResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; set; }
    public bool IsLiked { get; set; }
    public bool IsSaved { get; set; }
    public string Title { get; init; }
    public int CookingTime { get; init; }
    public string Description { get; init; }
    public string Username { get; init; }
    public int LikesCount { get; init; }
    public int CommentsCount { get; init; }
    public IEnumerable<GetPostTagResponse> Tags { get; init; }
    public IEnumerable<string> Images { get; init; }
    public IEnumerable<GetPostIngredientResponse> Ingredients { get; init; }
    public GetPostNutritionResponse Nutrition { get; init; }
    public IEnumerable<GetPostRecipeStepResponse> RecipeSteps { get; init; }
}
public record GetPostTagResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
}
public record GetPostIngredientResponse
{
    public Guid Id { get; init; }
    public int Quantity { get; init; }
    public string Name { get; init; } 
}

public record GetPostNutritionResponse
{
    public decimal? Calories { get; init; }
    public decimal? Fats { get; init; }
    public decimal? TotalCarbohydrates { get; init; }
    public decimal? Fiber { get; init; }
    public decimal? Sugars { get; init; }
    public decimal? Protein { get; init; }
    public decimal? Sodium { get; init; }
}
public record GetPostRecipeStepResponse
{
    public Guid Id { get; init; }
    public int StepNumber { get; init; }
    public string Description { get; init; }
    public string? Image { get; init; }
}
