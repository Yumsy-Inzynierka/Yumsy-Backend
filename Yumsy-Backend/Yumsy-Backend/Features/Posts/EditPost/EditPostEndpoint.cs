/*using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

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

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<EditPostResponse>> Handle(
        [FromRoute] Guid id,
        [FromBody] EditPostRequest editPostRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(editPostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var userId = User.GetUserId();
        
        var response = await _editPostHandler.Handle(id, editPostRequest, userId, cancellationToken);
            
        return Ok(response);
    }
}*/