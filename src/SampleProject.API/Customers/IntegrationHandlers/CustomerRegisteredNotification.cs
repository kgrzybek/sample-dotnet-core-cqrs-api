using System;
using Newtonsoft.Json;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
    {
        public CustomerId CustomerId { get; }

        public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
        {
            this.CustomerId = domainEvent.Customer.Id;
        }

        [JsonConstructor]
        public CustomerRegisteredNotification(CustomerId customerId) : base(null)
        {
            this.CustomerId = customerId;
        }
    }
}