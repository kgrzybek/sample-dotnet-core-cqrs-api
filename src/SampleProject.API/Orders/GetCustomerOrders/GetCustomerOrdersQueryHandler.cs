using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using SampleProject.Infrastructure;

namespace SampleProject.API.Orders.GetCustomerOrders
{
    internal class GetCustomerOrdersQueryHandler : IRequestHandler<GetCustomerOrdersQuery, List<OrderDto>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomerOrdersQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<List<OrderDto>> Handle(GetCustomerOrdersQuery request, CancellationToken cancellationToken)
        {
            using (var connection = this._sqlConnectionFactory.GetOpenConnection())
            {
                const string sql = "SELECT " +
                                   "[Order].[Id], " +
                                   "[Order].[IsRemoved], " +
                                   "[Order].[Value] " +
                                   "FROM orders.v_Orders AS [Order] " +
                                   "WHERE [Order].CustomerId = @CustomerId";
                var orders = await connection.QueryAsync<OrderDto>(sql, new {request.CustomerId});

                return orders.AsList();
            }
        }
    }
}