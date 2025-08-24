using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.Szablon;

namespace Yumsy_Backend.Features.Posts.LikePost;

// [Authorized]
[ApiController]
[Route("api/posts/{postId}/likes")]
public class LikePostEndpoint : ControllerBase
{
    private readonly LikePostHandler _handler;
    private readonly IValidator<LikePostRequest> _validator;
    
    public LikePostEndpoint(LikePostHandler handler, IValidator<LikePostRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<IActionResult> LikePost(
        [FromBody] LikePostRequest request,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _handler.Handle(request, cancellationToken);
        return Ok(result);
    }
}