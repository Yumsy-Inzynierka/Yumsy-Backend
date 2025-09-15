using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Profile.CreateProfile;

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
    
    [HttpPut]
    public async Task<IActionResult> AddProfileDetails([FromBody] AddProfileDetailsRequest addProfileDetailsRequest, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(addProfileDetailsRequest);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _addProfileDetailsHandler.Handle(addProfileDetailsRequest, cancellationToken);
            
        return NoContent();
    }
}