using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Extensions;

namespace Yumsy_Backend.Features.Users.Profile.EditProfileDetails;

[Authorize]
[ApiController]
[Route("api/profile")]
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
    public async Task<ActionResult> AddProfileDetails([FromBody] EditProfileDetailsRequest editProfileDetailsRequest, CancellationToken cancellationToken)
    {
        
        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        var userId = User.GetUserId();
        
        var fullRequest = new EditProfileDetailsRequest
        {
            Id = userId,
            ProfileName = editProfileDetailsRequest.ProfileName,
            ProfilePicture = editProfileDetailsRequest.ProfilePicture,
            Bio = editProfileDetailsRequest.Bio,
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