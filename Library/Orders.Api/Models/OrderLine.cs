namespace Orders.Api.Models;

public class OrderLine
{
    public long Id { get; set; }

    public Guid OrderId { get; set; }

    public long BookId { get; set; }

    public int Quantity { get; set; }
}
