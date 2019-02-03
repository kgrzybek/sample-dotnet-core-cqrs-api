using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders.Events
{
    public class OrderRemovedEvent : DomainEventBase
    {
        public Order Order { get; }

        public OrderRemovedEvent(Order order)
        {
            this.Order = order;
        }
    }
}