using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.SavePost;

//[Authorize]
[ApiController]
[Route("api/posts")]
public class SavePostEndpoint : ControllerBase
{
    private readonly SavePostHandler _savePostHandler;
    private readonly IValidator<SavePostRequest> _validator;
    
    public SavePostEndpoint(SavePostHandler savePostHandler, IValidator<SavePostRequest> validator)
    {
        _savePostHandler = savePostHandler;
        _validator = validator;
    }
    
    [HttpPost("{postId}/saved")]
    public async Task<ActionResult<SavePostResponse>> Handle([FromRoute] Guid postId, [FromBody]SavePostRequest savePostRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new SavePostRequest
        {
            PostId = postId,
            UserId = savePostRequest.UserId,
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _savePostHandler.Handle(fullRequest, cancellationToken);
            
        return Created($"api/posts/{response.PostId}/saved", response);
    }
}