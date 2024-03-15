using Orders.Api.Dto;
using Orders.Api.Queries;

namespace Orders.Api.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseWithPaggination<OrderDto>> GetOrdersAsync(GetOrdersQuery query, string? connectionString);
}
