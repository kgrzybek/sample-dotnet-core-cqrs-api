using Dapper;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Orders.GetCustomerOrders
{
    internal sealed class GetCustomerOrdersQueryHandler : IQueryHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerOrdersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            System.Data.IDbConnection connection = _sqlConnectionFactory.GetOpenConnection();
            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.v_Orders AS [Order] " +
                               "WHERE [Order].CustomerId = @CustomerId";
            IEnumerable<OrderDto> orders = await connection.QueryAsync<OrderDto>(sql, new { request.CustomerId });

            return orders.AsList();
        }
    }
}