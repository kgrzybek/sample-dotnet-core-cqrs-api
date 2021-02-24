using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public OrderRemovedEvent(OrderId orderId)
        {
            OrderId = orderId;
        }
    }
}