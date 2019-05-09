using System;
using Newtonsoft.Json;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
    {
        public Guid CustomerId { get; }

        public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
        {
            this.CustomerId = domainEvent.Customer.Id;
        }

        [JsonConstructor]
        public CustomerRegisteredNotification(Guid customerId) : base(null)
        {
            this.CustomerId = customerId;
        }
    }
}