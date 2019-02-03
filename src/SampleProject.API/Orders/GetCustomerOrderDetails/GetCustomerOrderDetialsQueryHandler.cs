using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using SampleProject.Infrastructure;

namespace SampleProject.API.Orders.GetCustomerOrderDetails
{
    internal class GetCustomerOrderDetialsQueryHandler : IRequestHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomerOrderDetialsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT " +
                                   "[Order].[Id], " +
                                   "[Order].[IsRemoved], " +
                                   "[Order].[Value] " +
                                   "FROM orders.v_Orders AS [Order] " +
                                   "WHERE [Order].Id = @OrderId";
                var order = await connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(sql, new {request.OrderId});

                const string sqlProducts = "SELECT " +
                                   "[Order].[ProductId] AS [Id], " +
                                   "[Order].[Quantity], " +
                                   "[Order].[Name] " +
                                   "FROM orders.v_OrderProducts AS [Order] " +
                                   "WHERE [Order].OrderId = @OrderId";
                var products = await connection.QueryAsync<ProductDto>(sqlProducts, new { request.OrderId });

                order.Products = products.AsList();

                return order;
            }
        }
    }
}