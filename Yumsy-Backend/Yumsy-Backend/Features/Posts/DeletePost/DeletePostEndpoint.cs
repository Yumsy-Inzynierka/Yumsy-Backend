using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.DeletePost;

[Authorize]
[ApiController]
[Route("api/posts")]
public class DeletePostEndpoint : ControllerBase
{
    private readonly DeletePostHandler _deletePostHandler;
    private readonly IValidator<DeletePostRequest> _validator;
    
    public DeletePostEndpoint(DeletePostHandler deletePostHandler, IValidator<DeletePostRequest> validator)
    {
        _deletePostHandler = deletePostHandler;
        _validator = validator;
    }
    
    [HttpDelete("{postId:guid}")]
    public async Task<IActionResult> Handle([FromRoute] DeletePostRequest deletePostRequest)
    {
        var validationResult = await _validator.ValidateAsync(deletePostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _deletePostHandler.Handle(deletePostRequest);
            
        return NoContent();
    }
}