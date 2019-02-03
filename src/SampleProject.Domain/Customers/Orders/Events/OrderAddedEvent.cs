using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderAddedEvent : DomainEventBase
    {
        public Order Order { get; }

        public OrderAddedEvent(Order order)
        {
            this.Order = order;
        }
    }
}