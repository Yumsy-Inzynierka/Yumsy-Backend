using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.EditPost;

[Authorize]
[ApiController]
[Route("api/posts")]
public class EditPostController : ControllerBase
{
    private readonly EditPostHandler _editPostHandler;
    private readonly IValidator<EditPostRequest> _validator;

    public EditPostController(EditPostHandler editPostHandler, IValidator<EditPostRequest> validator)
    {
        _editPostHandler = editPostHandler;
        _validator = validator;
    }

    [HttpPut("{postId:guid}")]
    public async Task<ActionResult> Handle([FromRoute] EditPostRequest editPostRequest, CancellationToken cancellationToken)
    {
        editPostRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(editPostRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _editPostHandler.Handle(editPostRequest, cancellationToken);
            
        return NoContent();
    }
}