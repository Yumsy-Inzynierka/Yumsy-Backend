using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Likes.LikePost;

[Authorize]
[ApiController]
[Route("api/posts")]
public class LikePostEndpoint : ControllerBase
{
    private readonly LikePostHandler _handler;
    private readonly IValidator<LikePostRequest> _validator;
    
    public LikePostEndpoint(LikePostHandler handler, IValidator<LikePostRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpPost("{postId:guid}/likes")]
    public async Task<ActionResult> LikePost(LikePostRequest likePostRequest, CancellationToken cancellationToken)
    {
        likePostRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(likePostRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await _handler.Handle(likePostRequest, cancellationToken);
        return NoContent();
    }
}