using FluentValidation;

namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.CreateOrder;

public static class CreateOrderEndpoint
{
    public static void MapCreateOrderEndpoint(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/orders", async (
            CreateOrderRequest request,
            CreateOrderHandler handler,
            IValidator<CreateOrderRequest> validator,
            CancellationToken ct) =>
        {
            var validationResult = await validator.ValidateAsync(request, ct);
            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors);

            var response = await handler.Handle(request, ct);
            return Results.Ok(response);
        });
    }
}
