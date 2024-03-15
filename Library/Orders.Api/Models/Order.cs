namespace Orders.Api.Models;

public class Order
{
    public Guid OrderId { get; set; }

    public IList<OrderLine> OrderLines { get; set; }
}
