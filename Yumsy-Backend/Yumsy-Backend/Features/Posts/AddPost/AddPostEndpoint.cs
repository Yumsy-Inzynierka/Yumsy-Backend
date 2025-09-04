using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.AddPost;

//[Authorize]
[ApiController]
[Route("api/posts")]
public class AddPostEndpoint : ControllerBase
{
    private readonly AddPostHandler _addPostHandler;
    private readonly IValidator<AddPostRequest> _validator;
    
    public AddPostEndpoint(AddPostHandler addPostHandler, IValidator<AddPostRequest> validator)
    {
        _addPostHandler = addPostHandler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<IActionResult> Handle([FromBody] AddPostRequest addPostRequest, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(addPostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _addPostHandler.Handle(addPostRequest, cancellationToken);
            
        return Created($"api/post/{response.Id}", response);
    }
}