using Newtonsoft.Json;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Customers.Orders.Events;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API.Orders.PlaceCustomerOrder
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