using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.Likes.LikeComment;

//[Authorize]
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
    
    [HttpPost("{commentId}/like")]
    public async Task<IActionResult> Handle([FromRoute] Guid commentId, [FromBody] LikeCommentRequest likeCommentRequest, CancellationToken cancellationToken)
    {
        var fullRequest = new LikeCommentRequest
        {
            CommentId = commentId,
            UserId = likeCommentRequest.UserId
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _likeCommentHandler.Handle(fullRequest, cancellationToken);
            
        return Created($"api/comments/{response.commentId}/like", response);
    }
}