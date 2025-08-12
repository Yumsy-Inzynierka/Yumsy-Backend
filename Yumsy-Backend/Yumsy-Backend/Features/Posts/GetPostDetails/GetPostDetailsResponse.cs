namespace Yumsy_Backend.Features.Posts.GetPostDetails;

public class GetPostDetailsResponse
{
    public Guid PostId { get; set; }
    public string Title { get; set; }
    public int CookingTime { get; set; }
    public string Description { get; set; } 
    public IEnumerable<GetPostIngredientResponse> Ingredients { get; set; }
    public decimal Calories { get; set; }
    public decimal Fats { get; set; }
    public decimal Carbohydrates { get; set; }
    public decimal? Fiber { get; set; }
    public decimal? Sugars { get; set; }
    public decimal Protein { get; set; }
    public decimal? Salt { get; set; }
    public IEnumerable<GetPostRecipeStepResponse> RecipeSteps { get; set; }
}
public class GetPostIngredientResponse
{
    public int Quantity { get; set; }
    public string Name { get; set; } 
}
public class GetPostRecipeStepResponse
{
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public string PhotoUrl { get; set; }
}
