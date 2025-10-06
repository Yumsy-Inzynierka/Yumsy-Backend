using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.AddPost;

public class AddPostRequest
{
    public Guid UserId { get; set; }

    [FromBody]
    public AddPostRequestBody Body { get; set; } = default!;
}

public class AddPostRequestBody
{
    public string Title { get; set; } = string.Empty;
    public int? CookingTime { get; set; }
    public string Description { get; set; } = string.Empty;
    public IEnumerable<AddPostRequestTag> Tags { get; set; } = Enumerable.Empty<AddPostRequestTag>();
    public IEnumerable<AddPostRequestImage> Images { get; set; } = Enumerable.Empty<AddPostRequestImage>();
    public IEnumerable<AddPostRequestIngredient> Ingredients { get; set; } = Enumerable.Empty<AddPostRequestIngredient>();
    public IEnumerable<AddPostRequestRecipeStep> RecipeSteps { get; set; } = Enumerable.Empty<AddPostRequestRecipeStep>();
}

public class AddPostRequestTag
{
    public Guid Id { get; set; }
}

public class AddPostRequestImage
{
    public string Image { get; set; } = string.Empty;
}

public class AddPostRequestIngredient
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}

public class AddPostRequestRecipeStep
{
    public int StepNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Image { get; set; }
}