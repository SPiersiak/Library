using Orders.Api.Queries;
using Orders.Api.Services.Interfaces;

namespace Orders.Api.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("api/orders", async ([AsParameters] GetOrdersQuery query, IConfiguration configuration, IOrderService orderService) =>
        {
            var connectionString = configuration.GetConnectionString("Library");

            var result = await orderService.GetOrdersAsync(query, connectionString);

            return Results.Ok(result);
        });
    }
}
