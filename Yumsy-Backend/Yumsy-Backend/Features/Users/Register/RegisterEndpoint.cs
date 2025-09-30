using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        await _handler.Handle(request);
        
        return Ok(new { message = "User registered successfully" });
    }
}

