using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

// [Authorized]
[ApiController]
[Route("api/profile")]
public class GetProfileDetailsEndpoint : ControllerBase
{
    private readonly GetProfileDetailsHandler _handler;
    private readonly IValidator<GetProfileDetailsRequest> _validator;
    
    public GetProfileDetailsEndpoint(GetProfileDetailsHandler handler, IValidator<GetProfileDetailsRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<ActionResult<GetProfileDetailsResponse>> GetProfileDetails(
        [FromRoute] GetProfileDetailsRequest request,
        CancellationToken cancellationToken
        )
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        GetProfileDetailsResponse response = await _handler.Handle(request);
            
        return Ok(response);
    }
}