using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.Szablon;

// [Authorized]
[ApiController]
[Route("api/")]
public class Endpoint : ControllerBase
{
    private readonly Handler _handler;
    private readonly IValidator<Request> _validator;
    
    public Endpoint(Handler handler, IValidator<Request> validator)
    {
        _handler = handler;
        _validator = validator;
    }
    
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> x(
        [FromRoute] Request request,
        // [FromBody] Request request,
        // [FromQuery] Request request,
        CancellationToken cancellationToken
        )
    {
        return Ok();
    }
}