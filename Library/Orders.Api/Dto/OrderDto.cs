namespace Orders.Api.Dto;

public class OrderDto
{
    public Guid OrderId { get; set; }

    public List<OrderLineDto> OrderLines { get; set; }
}
