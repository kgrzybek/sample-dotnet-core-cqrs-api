using Newtonsoft.Json;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class OrderPlacedNotification : DomainNotificationBase<OrderPlacedEvent>
    {
        public OrderId OrderId { get; }

        public OrderPlacedNotification(OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            this.OrderId = domainEvent.OrderId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId) : base(null)
        {
            this.OrderId = orderId;
        }
    }
}