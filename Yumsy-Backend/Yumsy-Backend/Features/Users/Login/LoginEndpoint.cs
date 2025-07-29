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
    public async Task<IActionResult> Handle([FromBody] LoginRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            LoginResponse response = await _handler.Handle(request);
            
            return Ok(response);
        }
        catch (PostgrestException ex)
        {
            return StatusCode(502, new { error = $"Supabase error: {ex.Message}" });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, new { error = $"Database error: {ex.Message}" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}