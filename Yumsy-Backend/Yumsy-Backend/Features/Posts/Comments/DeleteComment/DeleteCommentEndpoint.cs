using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Comments.DeleteComment;

[Authorize]
[ApiController]
[Route("api/posts/")]
public class DeleteCommentEndpoint : ControllerBase
{
    private readonly DeleteCommentHandler _handler;
    private readonly IValidator<DeleteCommentRequest> _validator;
    
    public DeleteCommentEndpoint(DeleteCommentHandler handler, IValidator<DeleteCommentRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpDelete("{postId:guid}/comments/{commentId:guid}")]
    public async Task<IActionResult> DeleteComment([FromRoute] DeleteCommentRequest deleteCommentRequest, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(deleteCommentRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await _handler.Handle(deleteCommentRequest, cancellationToken);

        return NoContent();
    }
}