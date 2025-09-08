using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supabase.Postgrest.Exceptions;

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
    public async Task<ActionResult<LoginResponse>> Handle([FromBody] LoginRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        LoginResponse response = await _handler.Handle(request);
            
        return Ok(response);
    }
}