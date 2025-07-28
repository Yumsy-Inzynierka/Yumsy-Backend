
using Microsoft.AspNetCore.Mvc;
using Yumsy_Backend.Features.Users.Register;

[ApiController]
[Route("auth/register")]
public class RegisterController : ControllerBase
{
    private readonly RegisterHandler _handler;

    public RegisterController(RegisterHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        RegisterResponse response = await _handler.Handle(request);
        if (!response.Success)
            return BadRequest(new { error = response.Message });

        return Ok(response);
    }
}

