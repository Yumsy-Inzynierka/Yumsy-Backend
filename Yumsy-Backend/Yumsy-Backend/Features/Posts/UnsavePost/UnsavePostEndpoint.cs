using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.UnsavePost;

[Authorize]
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
    
    [HttpDelete("{postId:guid}/unsave")]
    public async Task<IActionResult> Handle([FromRoute] UnsavePostRequest unsavePostRequest, CancellationToken cancellationToken)
    {
        unsavePostRequest.UserId = User.GetUserId(); 
        
        var validationResult = await _validator.ValidateAsync(unsavePostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unsavePostHandler.Handle(unsavePostRequest, cancellationToken);
            
        return NoContent();
    }
}