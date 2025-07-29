using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.CreateOrder;


[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrderHandler _handler;
    private readonly IValidator<CreateOrderRequest> _validator;

    public OrdersController(CreateOrderHandler handler, IValidator<CreateOrderRequest> validator)
    {
        _handler = handler;
        _validator = validator;
    }

    /*[HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken ct)
    {
        /*var validationResult = await _validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var response = await _handler.Handle(request, ct);
        return Ok(response);#1#
    }*/
}
