namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public record GetPostDetailsResponse
{
    public Guid PostId { get; init; }
    public string Title { get; init; }
    public int CookingTime { get; init; }
    public string Description { get; init; } 
    public IEnumerable<GetPostIngredientResponse> Ingredients { get; init; }
    public decimal Calories { get; init; }
    public decimal Fats { get; init; }
    public decimal Carbohydrates { get; init; }
    public decimal? Fiber { get; init; }
    public decimal? Sugars { get; init; }
    public decimal Protein { get; init; }
    public decimal? Salt { get; init; }
    public IEnumerable<GetPostRecipeStepResponse> RecipeSteps { get; init; }
}
public record GetPostIngredientResponse
{
    public int Quantity { get; init; }
    public string Name { get; init; } 
}
public record GetPostRecipeStepResponse
{
    public int StepNumber { get; init; }
    public string Description { get; init; }
    public string PhotoUrl { get; init; }
}
