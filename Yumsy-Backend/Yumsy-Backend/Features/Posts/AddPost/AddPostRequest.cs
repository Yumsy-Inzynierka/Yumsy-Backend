using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.AddPost;

public class AddPostRequest
{
    [FromBody] 
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public int? CookingTime { get; set; }
    public string Description { get; set; }
    public IEnumerable<AddPostRequestTag> Tags { get; set; }
    public IEnumerable<AddPostRequestImage> Images { get; set; }
    public IEnumerable<AddPostRequestIngredient> Ingredients { get; set; }
    public IEnumerable<AddPostRequestRecipeStep> RecipeSteps { get; set; }
}

public class AddPostRequestTag
{
    public Guid Id { get; set; }
}

public class AddPostRequestImage
{
    public string Image { get; set; }
}

public class AddPostRequestIngredient
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}

public class AddPostRequestRecipeStep
{
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
}
