using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supabase.Postgrest.Exceptions;
using Yumsy_Backend.Features.Users.Register;

[ApiController]
[Route("auth/register")]
public class RegisterController : ControllerBase
{
    private readonly RegisterHandler _handler;
    private readonly IValidator<RegisterRequest> _validator;

    public RegisterController(RegisterHandler handler, IValidator<RegisterRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }
            await _handler.Handle(request);
            return Ok(new { message = "User registered successfully" });
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

