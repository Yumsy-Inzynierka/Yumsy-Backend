using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetSavedPosts;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetSavedPostsController : ControllerBase
{
    private readonly GetSavedPostsHandler _getSavedPostsHandler;
    private readonly IValidator<GetSavedPostsRequest> _validator;

    public GetSavedPostsController(GetSavedPostsHandler getSavedPostsHandler, IValidator<GetSavedPostsRequest> validator)
    {
        _getSavedPostsHandler = getSavedPostsHandler;
        _validator = validator;
    }

    [HttpGet("saved")]
    public async Task<ActionResult<GetSavedPostsResponse>> Handle(
        [FromRoute] GetSavedPostsRequest getSavedPostsRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(getSavedPostsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var userId = User.GetUserId();
        
        var response = await _getSavedPostsHandler.Handle(getSavedPostsRequest, userId, cancellationToken);
            
        return Ok(response);
    }
}