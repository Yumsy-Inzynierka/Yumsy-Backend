using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Comments.DeleteComment;

// [Authorized]
[ApiController]
[Route("api/posts/{postId}/comments")]
public class DeleteCommentEndpoint : ControllerBase
{
    private readonly DeleteCommentHandler _handler;
    private readonly IValidator<DeleteCommentRequest> _validator;
    
    public DeleteCommentEndpoint(DeleteCommentHandler handler, IValidator<DeleteCommentRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpDelete]
    [Route("{commentId}")]
    public async Task<IActionResult> DeleteComment(
        [FromRoute] DeleteCommentRequest request,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var userId = User.GetUserId();
        
        await _handler.Handle(request, userId, cancellationToken);

        return NoContent();
    }
}