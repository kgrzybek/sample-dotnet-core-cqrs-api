using System;
using MediatR;

namespace SampleProject.API.Orders.GetCustomerOrderDetails
{
    internal class GetCustomerOrderDetailsQuery : IRequest<OrderDetailsDto>
    {
        public Guid OrderId { get; }

        public GetCustomerOrderDetailsQuery(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}