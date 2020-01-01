using System;
using MediatR;

namespace SampleProject.Application.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public RemoveCustomerOrderCommand(
            Guid customerId,
            Guid orderId)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
        }
    }
}