using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<GetTopDailyPostsResponse>> GetTopDailyPosts(CancellationToken cancellationToken)
    {
        var response = await _getTopDailyPostsHandler.Handle(cancellationToken);
            
        return Ok(response);
    }
}