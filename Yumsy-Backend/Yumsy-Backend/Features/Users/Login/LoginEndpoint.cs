using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.Login;

[ApiController]
[Route("auth/login")]
public class LoginEndpoint : ControllerBase
{
    private readonly LoginHandler _handler;
    private readonly IValidator<LoginRequest> _validator;

    public LoginEndpoint(LoginHandler handler, IValidator<LoginRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Handle([FromBody] LoginRequest request)
    {
        var validation = await _validator.ValidateAsync(request);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        LoginResponse response = await _handler.Handle(request);

        if (!response.Success || response == null)
            return Unauthorized(new { error = response.Message });

        return Ok(response);
    }
}