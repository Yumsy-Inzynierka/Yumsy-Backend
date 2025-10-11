using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.GetUserProfileDetails;

[Authorize]
[ApiController]
[Route("api/profiles")]
public class GetUserProfileDetailsEndpoint : ControllerBase
{
    private readonly GetUserProfileDetailsHandler _handler;
    private readonly IValidator<GetUserProfileDetailsRequest> _validator;
    
    public GetUserProfileDetailsEndpoint(GetUserProfileDetailsHandler handler, IValidator<GetUserProfileDetailsRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<ActionResult<GetUserProfileDetailsResponse>> GetProfileDetails([FromRoute] GetUserProfileDetailsRequest getUserProfileDetailsRequest, CancellationToken cancellationToken)
    {
         
        var validationResult = await _validator.ValidateAsync(getUserProfileDetailsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        GetUserProfileDetailsResponse response = await _handler.Handle(getUserProfileDetailsRequest);
            
        return Ok(response);
    }
}