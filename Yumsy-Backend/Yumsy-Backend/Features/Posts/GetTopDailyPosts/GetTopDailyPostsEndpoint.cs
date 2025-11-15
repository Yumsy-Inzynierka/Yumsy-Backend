using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetTopDailyPosts;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetTopDailyPostsController : ControllerBase
{
    private readonly GetTopDailyPostsHandler _getTopDailyPostsHandler;
    private readonly IValidator<GetTopDailyPostsRequest> _validator;

    public GetTopDailyPostsController(GetTopDailyPostsHandler getTopDailyPostsHandler, IValidator<GetTopDailyPostsRequest> validator)
    {
        _getTopDailyPostsHandler = getTopDailyPostsHandler;
        _validator = validator;
    }

    [HttpGet("top-daily")]
    public async Task<ActionResult<GetTopDailyPostsResponse>> GetTopDailyPosts(
        [FromRoute] GetTopDailyPostsRequest request,
        CancellationToken cancellationToken)
    {
        request.UserId = User.GetUserId();
        
        var response = await _getTopDailyPostsHandler.Handle(request, cancellationToken);
            
        return Ok(response);
    }
}