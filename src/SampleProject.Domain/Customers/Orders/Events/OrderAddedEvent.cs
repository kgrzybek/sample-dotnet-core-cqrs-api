using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderAddedEvent : DomainEventBase
    {
        public Guid OrderId { get; }

        public OrderAddedEvent(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }
}