using SampleProject.Application.Configuration.Queries;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IQuery<List<OrderDto>>
    {
        public Guid CustomerId { get; }

        public GetCustomerOrdersQuery(Guid customerId)
        {
            CustomerId = customerId;
        }
    }
}