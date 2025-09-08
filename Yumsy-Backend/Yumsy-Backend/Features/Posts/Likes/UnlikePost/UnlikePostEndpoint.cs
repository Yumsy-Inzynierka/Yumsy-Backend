using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.UnlikePost;

// [Authorized]
[ApiController]
[Route("api/posts/{postId:guid}/likes")]
public class UnlikePostEndpoint : ControllerBase
{
    private readonly UnlikePostHandler _handler;
    private readonly IValidator<UnlikePostRequest> _validator;
    
    public UnlikePostEndpoint(UnlikePostHandler handler, IValidator<UnlikePostRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpDelete]
    public async Task<ActionResult<UnlikePostResponse>> UnlikePost(
        [FromRoute] UnlikePostRequest request,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var userId = User.GetUserId();

        var result = await _handler.Handle(request, userId, cancellationToken);
        return Ok(result);
    }
}