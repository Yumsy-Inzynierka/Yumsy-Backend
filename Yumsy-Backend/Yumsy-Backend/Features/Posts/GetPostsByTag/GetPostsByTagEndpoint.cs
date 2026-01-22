using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetPostsByTag;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetPostsByTagController : ControllerBase
{
    private readonly GetPostsByTagHandler _getPostsByTagHandler;
    private readonly IValidator<GetPostsByTagRequest> _validator;

    public GetPostsByTagController(GetPostsByTagHandler getPostsByTagHandler, IValidator<GetPostsByTagRequest> validator)
    {
        _getPostsByTagHandler = getPostsByTagHandler;
        _validator = validator;
    }

    [HttpGet("by-tag")]
    public async Task<ActionResult<GetPostsByTagResponse>> Handle([FromQuery] GetPostsByTagRequest getPostsByTagRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(getPostsByTagRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var userId = User.GetUserId();
        
        var response = await _getPostsByTagHandler.Handle(getPostsByTagRequest, userId, cancellationToken);
            
        return Ok(response);
    }
}