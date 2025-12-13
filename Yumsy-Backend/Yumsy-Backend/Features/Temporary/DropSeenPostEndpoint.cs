using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Temporary.DropSeenPost;

[Authorize]
[ApiController]
[Route("api/seen-post")]
public class DropSeenPostEndpoint : ControllerBase
{
    private readonly DropSeenPostHandler _dropSeenPostHandler;
    
    public DropSeenPostEndpoint(DropSeenPostHandler dropSeenPostHandler)
    {
        _dropSeenPostHandler = dropSeenPostHandler;
    }
    
    [HttpDelete]
    public async Task<IActionResult> Handle([FromRoute] DropSeenPostRequest dropSeenPostRequest)
    {
        dropSeenPostRequest.UserId = User.GetUserId();
        
        await _dropSeenPostHandler.Handle(dropSeenPostRequest);
            
        return NoContent();
    }
}