using Dapper;
using Mapster;
using Microsoft.Data.SqlClient;
using Orders.Api.Dto;
using Orders.Api.Models;
using Orders.Api.Queries;
using Orders.Api.Services.Interfaces;

namespace Orders.Api.Services;

public class OrderService : IOrderService
{
    public async Task<ResponseWithPaggination<OrderDto>> GetOrdersAsync(GetOrdersQuery query, string? connectionString)
    {
        await using var connection = new SqlConnection(connectionString);
        const string sql = """
                        SELECT t.OrderId, ol.Id, ol.OrderId, ol.BookId, ol.Quantity
                        FROM (
                            SELECT o.OrderId FROM Orders AS o
                            ORDER BY (SELECT 1) OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY) AS t
                        LEFT JOIN OrderLines AS ol ON t.OrderId = ol.[OrderId] ORDER BY t.OrderId
                        """;

        var orderDictionary = new Dictionary<Guid, Order>();

        var parameters = new { Skip = (query.PageNamber - 1) * query.PageSize, Take = query.PageSize };
        var orderList = connection.Query<Order, OrderLine, Order>(
            sql,
            (orderQuery, orderLineQuery) =>
            {
                Order? order;
                if (!orderDictionary.TryGetValue(orderQuery.OrderId, out order))
                {
                    orderDictionary.Add(orderQuery.OrderId, order = orderQuery);
                }
                if (order.OrderLines == null)
                {
                    order.OrderLines = new List<OrderLine>();
                }
                order.OrderLines.Add(orderLineQuery);

                return order;
            }, splitOn: "OrderId", param: parameters).Distinct().ToList();

        // przy milionowych ilościach wpisów to rozwiązanie bedzie nieporównywalnie szybsze aniżeli każdorazowy Count na tabeli Orders
        const string sql2 = """
            SELECT TotalOrderQuantity FROM OrderAgr
            """;
        var totalCount = connection.QueryFirst<int>(sql2);

        return new ResponseWithPaggination<OrderDto>
        {
            Page = query.PageNamber,
            PageSize = query.PageSize,
            Result = orderList.Adapt<List<OrderDto>>(),
            Total = totalCount,
        };
    }
}
