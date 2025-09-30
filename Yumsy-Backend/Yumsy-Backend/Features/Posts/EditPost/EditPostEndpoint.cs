using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.EditPost;

//[Authorize]
[ApiController]
[Route("api/post")]
public class Controller : ControllerBase
{
    private readonly EditPostHandler _editPostHandler;
    private readonly IValidator<EditPostRequest> _validator;

    public Controller(EditPostHandler editPostHandler, IValidator<EditPostRequest> validator)
    {
        _editPostHandler = editPostHandler;
        _validator = validator;
    }

    [HttpPut("{postId:guid}")]
    public async Task<ActionResult> Handle([FromRoute] EditPostRequest editPostRequest, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(editPostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _editPostHandler.Handle(editPostRequest, cancellationToken);
            
        return NoContent();
    }
}