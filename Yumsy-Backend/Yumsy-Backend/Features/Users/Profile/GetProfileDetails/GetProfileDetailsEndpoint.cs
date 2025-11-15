using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.GetProfileDetails;

[Authorize]
[ApiController]
[Route("api/profiles")]
public class GetProfileDetailsEndpoint : ControllerBase
{
    private readonly GetProfileDetailsHandler _handler;
    private readonly IValidator<GetProfileDetailsRequest> _validator;
    
    public GetProfileDetailsEndpoint(GetProfileDetailsHandler handler, IValidator<GetProfileDetailsRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet("{profileOwnerId:guid}")]
    public async Task<ActionResult<GetProfileDetailsResponse>> GetProfileDetails([FromRoute] GetProfileDetailsRequest getProfileDetailsRequest, CancellationToken cancellationToken)
    {
        getProfileDetailsRequest.UserId = User.GetUserId(); 
        
        var validationResult = await _validator.ValidateAsync(getProfileDetailsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        GetProfileDetailsResponse response = await _handler.Handle(getProfileDetailsRequest, cancellationToken);
            
        return Ok(response);
    }
}