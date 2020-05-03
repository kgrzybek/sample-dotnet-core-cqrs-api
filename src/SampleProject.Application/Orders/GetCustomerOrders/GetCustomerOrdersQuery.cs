using System;
using System.Collections.Generic;
using MediatR;
using SampleProject.Application.Configuration.Queries;

namespace SampleProject.Application.Orders.GetCustomerOrders
{
    public class GetCustomerOrdersQuery : IQuery<List<OrderDto>>
    {
        public Guid CustomerId { get; }

        public GetCustomerOrdersQuery(Guid customerId)
        {
            this.CustomerId = customerId;
        }
    }
}