using System;
using MediatR;

namespace SampleProject.Application.Orders.GetCustomerOrderDetails
{
    public class GetCustomerOrderDetailsQuery : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; }

        public GetCustomerOrderDetailsQuery(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}