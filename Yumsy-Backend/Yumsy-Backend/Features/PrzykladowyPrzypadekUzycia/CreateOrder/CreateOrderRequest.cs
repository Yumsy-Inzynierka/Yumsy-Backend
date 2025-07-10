namespace Yumsy_Backend.Features.PrzykladowyPrzypadekUzycia.CreateOrder;

public class CreateOrderRequest
{
    public string CustomerId { get; set; }
    public List<string> ProductIds { get; set; } = new();
}