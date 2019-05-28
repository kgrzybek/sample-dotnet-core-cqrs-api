using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderAddedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderAddedEvent(OrderId orderId)
        {
            this.OrderId = orderId;
        }
    }
}