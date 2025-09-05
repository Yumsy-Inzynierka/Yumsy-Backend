using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

//[Authorize]
[ApiController]
[Route("api/comments")]
public class UnlikeCommentEndpoint : ControllerBase
{
    private readonly UnlikeCommentHandler _unlikeCommentHandler;
    private readonly IValidator<UnlikeCommentRequest> _validator;
    
    public UnlikeCommentEndpoint(UnlikeCommentHandler unlikeCommentHandler, IValidator<UnlikeCommentRequest> validator)
    {
        _unlikeCommentHandler = unlikeCommentHandler;
        _validator = validator;
    }
    
    [HttpDelete("{commentId}/unlike")]
    public async Task<IActionResult> Handle([FromRoute] Guid commentId, [FromBody] UnlikeCommentRequest unlikeCommentRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new UnlikeCommentRequest
        {
            CommentId = commentId,
            UserId = unlikeCommentRequest.UserId
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unlikeCommentHandler.Handle(fullRequest, cancellationToken);
            
        return NoContent();
    }
}