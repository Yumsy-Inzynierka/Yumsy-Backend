namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.CreateOrder;

public class CreateOrderHandler
{
    private readonly AppDbContext _db;

    public CreateOrderHandler(AppDbContext db)
    {
        _db = db;
    }

    public async Task<CreateOrderResponse> Handle(CreateOrderRequest request, CancellationToken ct)
    {
        var order = new Order
        {
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            Products = request.ProductIds.Select(id => new OrderProduct { ProductId = id }).ToList()
        };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync(ct);

        return new CreateOrderResponse(order.Id);
    }
}
