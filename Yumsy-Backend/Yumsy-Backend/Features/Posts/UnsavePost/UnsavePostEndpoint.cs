using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

//[Authorize]
[ApiController]
[Route("api/posts")]
public class UnsavePostEndpoint : ControllerBase
{
    private readonly UnsavePostHandler _unsavePostHandler;
    private readonly IValidator<UnsavePostRequest> _validator;
    
    public UnsavePostEndpoint(UnsavePostHandler unsavePostHandler, IValidator<UnsavePostRequest> validator)
    {
        _unsavePostHandler = unsavePostHandler;
        _validator = validator;
    }
    
    [HttpDelete("{postId}/unsave")]
    public async Task<IActionResult> Handle([FromRoute] Guid postId, [FromBody]UnsavePostRequest unsavePostRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new UnsavePostRequest
        {
            PostId = postId,
            UserId = unsavePostRequest.UserId,
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unsavePostHandler.Handle(fullRequest, cancellationToken);
            
        return NoContent();
    }
}