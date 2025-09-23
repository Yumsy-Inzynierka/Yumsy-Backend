using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

[ApiController]
[Route("auth/refresh-token")]
public class RefreshTokenEndpoint : ControllerBase
{
    private readonly RefreshTokenHandler _handler;
    private readonly IValidator<RefreshTokenRequest> _validator;

    public RefreshTokenEndpoint(RefreshTokenHandler handler, IValidator<RefreshTokenRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpPost]
    public async Task<ActionResult<RefreshTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var result = await _handler.Handle(request);
        return Ok(result);
    }
}