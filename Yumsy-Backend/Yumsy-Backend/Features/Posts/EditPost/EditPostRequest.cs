using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.EditPost;
public class EditPostRequest
{
    public Guid UserId { get; set; }
    
    [FromRoute(Name = "postId")]
    public Guid PostId { get; set; }
    

    [FromBody]
    public EditPostRequestBody Body { get; set; } = default!;
}

public class EditPostRequestBody
{
    public string Title { get; set; }
    public int? CookingTime { get; set; }
    public string Description { get; set; }
    public IEnumerable<AddPostRequestTagBody> Tags { get; set; }
    public IEnumerable<EditPostRequestImageBody> Images { get; set; }
    public IEnumerable<EditPostRequestIngredientBody> Ingredients { get; set; }
    public IEnumerable<EditPostRequestRecipeStepBody> RecipeSteps { get; set; }
}

public class AddPostRequestTagBody
{
    public Guid Id { get; set; }
}

public class EditPostRequestImageBody
{
    public string Image { get; set; }
}

public class EditPostRequestIngredientBody
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}

public class EditPostRequestRecipeStepBody
{
    public int StepNumber { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
}
