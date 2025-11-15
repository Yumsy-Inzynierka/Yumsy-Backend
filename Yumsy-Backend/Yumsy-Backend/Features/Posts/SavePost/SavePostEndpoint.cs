using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.SavePost;

[Authorize]
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
    
    [HttpPost("{postId:guid}/save")]
    public async Task<ActionResult<SavePostResponse>> Handle([FromRoute] SavePostRequest savePostRequest, CancellationToken cancellationToken)
    {
        savePostRequest.UserId = User.GetUserId();
        var validationResult = await _validator.ValidateAsync(savePostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _savePostHandler.Handle(savePostRequest, cancellationToken);
            
        return NoContent();
    }
}