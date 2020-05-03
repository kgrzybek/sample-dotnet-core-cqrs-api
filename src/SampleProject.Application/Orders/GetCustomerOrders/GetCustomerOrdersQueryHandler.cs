using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using SampleProject.Application.Configuration.Data;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrders
{
    internal sealed class GetCustomerOrdersQueryHandler : IQueryHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerOrdersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();
                const string sql = "SELECT " +
                                   "[Order].[Id], " +
                                   "[Order].[IsRemoved], " +
                                   "[Order].[Value], " +
                                   "[Order].[Currency] " +
                                   "FROM orders.v_Orders AS [Order] " +
                                   "WHERE [Order].CustomerId = @CustomerId";
                var orders = await connection.QueryAsync<OrderDto>(sql, new {request.CustomerId});

                return orders.AsList();
        }
    }
}