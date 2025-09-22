using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;
using Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

namespace Yumsy_Backend.Features.Users.Profile.AddProfileDetails;

// [Authorized]
[ApiController]
[Route("api/profile")]
public class AddProfileDetailsEndpoint : ControllerBase
{
    private readonly AddProfileDetailsHandler _addProfileDetailsHandler;
    private readonly IValidator<AddProfileDetailsRequest> _validator;
    
    public AddProfileDetailsEndpoint(AddProfileDetailsHandler addProfileDetailsHandler, IValidator<AddProfileDetailsRequest> validator)
    {
        _addProfileDetailsHandler = addProfileDetailsHandler;
        _validator = validator;
    }
    
    [HttpPost]
    public async Task<ActionResult> AddProfileDetails([FromBody] AddProfileDetailsRequest addProfileDetailsRequest, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId(); 
        
        var fullRequest = new AddProfileDetailsRequest
        {
            Id = userId,
            ProfileName = addProfileDetailsRequest.ProfileName,
            ProfilePicture = addProfileDetailsRequest.ProfilePicture,
            Bio = addProfileDetailsRequest.Bio,
        };
        
        var validationResult = await _validator.ValidateAsync(fullRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _addProfileDetailsHandler.Handle(fullRequest, cancellationToken);
            
        return NoContent();
    }
}