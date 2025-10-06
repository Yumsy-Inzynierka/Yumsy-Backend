using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Tags.GetTopDailyTags;

[Authorize]
[ApiController]
[Route("api/tags")]
public class GetTopDailyTagsController : ControllerBase
{
    private readonly GetTopDailyTagsHandler _getTopDailyTagsHandler;
    private readonly IValidator<GetTopDailyTagsRequest> _validator;

    public GetTopDailyTagsController(GetTopDailyTagsHandler getTopDailyTagsHandler, IValidator<GetTopDailyTagsRequest> validator)
    {
        _getTopDailyTagsHandler = getTopDailyTagsHandler;
        _validator = validator;
    }

    [HttpGet("top-daily")]
    public async Task<ActionResult<GetTopDailyTagsResponse>> GetTopDailyTags(CancellationToken cancellationToken)
    {
        var response = await _getTopDailyTagsHandler.Handle(cancellationToken);
            
        return Ok(response);
    }
}