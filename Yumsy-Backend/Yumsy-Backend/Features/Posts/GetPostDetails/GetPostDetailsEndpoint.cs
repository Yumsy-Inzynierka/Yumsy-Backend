using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Posts.GetPostDetails;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetPostDetailsEndpoint : ControllerBase
{
    private readonly GetPostDetailsHandler _getPostDetailsRequest;
    private readonly IValidator<GetPostDetailsRequest> _validator;
    
    public GetPostDetailsEndpoint(GetPostDetailsHandler getPostDetailsRequest, IValidator<GetPostDetailsRequest> validator)
    {
        _getPostDetailsRequest = getPostDetailsRequest;
        _validator = validator;
    }
    
    [HttpGet("{postId:guid}")]
    public async Task<ActionResult<GetPostDetailsResponse>> Handle([FromRoute] GetPostDetailsRequest detailsRequest)
    {
        detailsRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(detailsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        GetPostDetailsResponse detailsResponse = await _getPostDetailsRequest.Handle(detailsRequest);
            
        return Ok(detailsResponse);
    }
}