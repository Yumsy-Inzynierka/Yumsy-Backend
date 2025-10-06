using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.Likes.UnlikePost;

[Authorize]
[ApiController]
[Route("api/posts")]
public class UnlikePostEndpoint : ControllerBase
{
    private readonly UnlikePostHandler _handler;
    private readonly IValidator<UnlikePostRequest> _validator;
    
    public UnlikePostEndpoint(UnlikePostHandler handler, IValidator<UnlikePostRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpDelete("{postId:guid}/likes")]
    public async Task<ActionResult<UnlikePostResponse>> UnlikePost([FromRoute] UnlikePostRequest unlikePostRequest,CancellationToken cancellationToken)
    {
        unlikePostRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(unlikePostRequest, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _handler.Handle(unlikePostRequest, cancellationToken);
        return Ok(result);
    }
}