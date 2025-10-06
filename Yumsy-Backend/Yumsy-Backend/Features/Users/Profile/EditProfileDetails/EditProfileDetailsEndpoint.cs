using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

[Authorize]
[ApiController]
[Route("api/profiles")]
public class EditProfileDetailsEndpoint : ControllerBase
{
    private readonly EditProfileDetailsHandler _addProfileDetailsHandler;
    private readonly IValidator<EditProfileDetailsRequest> _validator;
    
    public EditProfileDetailsEndpoint(EditProfileDetailsHandler editProfileDetailsHandler, IValidator<EditProfileDetailsRequest> validator)
    {
        _addProfileDetailsHandler = editProfileDetailsHandler;
        _validator = validator;
    }
    
    [HttpPut]
    public async Task<ActionResult> AddProfileDetails([FromRoute] EditProfileDetailsRequest editProfileDetailsRequest, CancellationToken cancellationToken)
    {
        editProfileDetailsRequest.UserId = User.GetUserId();
        
        var validationResult = await _validator.ValidateAsync(editProfileDetailsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _addProfileDetailsHandler.Handle(editProfileDetailsRequest, cancellationToken);
            
        return NoContent();
    }
}