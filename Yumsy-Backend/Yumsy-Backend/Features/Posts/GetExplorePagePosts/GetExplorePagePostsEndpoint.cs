using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetExplorePagePosts;

[Authorize]
[ApiController]
[Route("api/posts/")]
public class GetExplorePagePostsController : ControllerBase
{
    private readonly GetExplorePagePostsHandler _getExplorePagePostsHandler;
    private readonly IValidator<GetExplorePagePostsRequest> _validator;

    public GetExplorePagePostsController(GetExplorePagePostsHandler getExplorePagePostsHandler, IValidator<GetExplorePagePostsRequest> validator)
    {
        _getExplorePagePostsHandler = getExplorePagePostsHandler;
        _validator = validator;
    }

    [HttpGet("explore-page")]
    public async Task<ActionResult<GetExplorePagePostsResponse>> Handle([FromRoute] GetExplorePagePostsRequest getExplorePagePostsRequest, CancellationToken cancellationToken)
    {
        getExplorePagePostsRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(getExplorePagePostsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _getExplorePagePostsHandler.Handle(getExplorePagePostsRequest, cancellationToken);
            
        return Ok(response);
    }
}