using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment;

[Authorize]
[ApiController]
[Route("api/comments")]
public class LikeCommentEndpoint : ControllerBase
{
    private readonly LikeCommentHandler _likeCommentHandler;
    private readonly IValidator<LikeCommentRequest> _validator;
    
    public LikeCommentEndpoint(LikeCommentHandler likeCommentHandler, IValidator<LikeCommentRequest> validator)
    {
        _likeCommentHandler = likeCommentHandler;
        _validator = validator;
    }
    
    [HttpPost("{commentId:guid}/likes")]
    public async Task<IActionResult> Handle([FromRoute] LikeCommentRequest likeCommentRequest, CancellationToken cancellationToken)
    {
        likeCommentRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(likeCommentRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _likeCommentHandler.Handle(likeCommentRequest, cancellationToken);
            
        return NoContent();
    }
}