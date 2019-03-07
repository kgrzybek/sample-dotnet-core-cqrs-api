using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class CustomerRegisteredNotification : DomainNotificationBase<CustomerRegisteredEvent>
    {
        public CustomerRegisteredNotification(CustomerRegisteredEvent domainEvent) : base(domainEvent)
        {
        }
    }
}