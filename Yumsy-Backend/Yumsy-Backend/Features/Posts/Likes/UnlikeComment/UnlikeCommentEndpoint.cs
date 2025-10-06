using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikeComment;

[Authorize]
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
    
    [HttpDelete("{commentId:guid}/unlike")]
    public async Task<IActionResult> Handle([FromRoute] UnlikeCommentRequest unlikeCommentRequest, CancellationToken cancellationToken)
    {
        unlikeCommentRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(unlikeCommentRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _unlikeCommentHandler.Handle(unlikeCommentRequest, cancellationToken);
            
        return NoContent();
    }
}