using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.Users.RefreshTokenEndpoint;

[ApiController]
[Route("auth/refresh-token")]
public class RefreshTokenEndpoint : ControllerBase
{
    private readonly RefreshTokenHandler _handler;

    public RefreshTokenEndpoint(RefreshTokenHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<ActionResult<RefreshTokenResponse>> Post([FromBody] RefreshTokenRequest request)
    {
        var result = await _handler.Handle(request);
        return Ok(result);
    }
}