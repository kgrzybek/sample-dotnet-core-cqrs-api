using SampleProject.Application.Configuration.Commands;
using System;

namespace SampleProject.Application.Orders.RemoveCustomerOrder
{
    public class RemoveCustomerOrderCommand : CommandBase
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public RemoveCustomerOrderCommand(
            Guid customerId,
            Guid orderId)
        {
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}