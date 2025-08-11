using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Posts.GetPost;

[Authorize]
[ApiController]
[Route("api/posts")]
public class GetPostDetailsEndpoint : ControllerBase
{
    private readonly GetPostDetailsHandler _detailsHandler;
    private readonly IValidator<GetPostDetailsRequest> _validator;
    
    public GetPostDetailsEndpoint(GetPostDetailsHandler detailsHandler, IValidator<GetPostDetailsRequest> validator)
    {
        _detailsHandler = detailsHandler;
        _validator = validator;
    }
    
    [HttpGet]
    [Route("{postId}")]
    public async Task<IActionResult> Handle([FromRoute] GetPostDetailsRequest detailsRequest)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(detailsRequest);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            GetPostDetailsResponse detailsResponse = await _detailsHandler.Handle(detailsRequest);
            
            return Ok(detailsResponse);
        }catch (ArgumentException ex) 
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}