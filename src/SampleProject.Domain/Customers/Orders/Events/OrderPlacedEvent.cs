using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderPlacedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderPlacedEvent(OrderId orderId)
        {
            this.OrderId = orderId;
        }
    }
}