using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Tags.GetTags;

[Authorize]
[ApiController]
[Route("api/")]
public class GetTagsController : ControllerBase
{
    private readonly GetTagsHandler _getTagsHandler;
    private readonly IValidator<GetTagsRequest> _validator;

    public GetTagsController(GetTagsHandler getTagsHandler, IValidator<GetTagsRequest> validator)
    {
        _getTagsHandler = getTagsHandler;
        _validator = validator;
    }

    [HttpGet("tags")]
    public async Task<ActionResult<GetTagsResponse>> Handle([FromRoute] GetTagsRequest getTagsRequest,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(getTagsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var response = await _getTagsHandler.Handle(getTagsRequest, cancellationToken);
            
        return Ok(response);
    }
}