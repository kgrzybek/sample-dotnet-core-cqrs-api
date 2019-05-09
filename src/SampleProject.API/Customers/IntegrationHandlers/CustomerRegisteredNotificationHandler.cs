using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Infrastructure.Customers;

namespace SampleProject.API.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerRegisteredNotificationHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            // This logic is executed outside transaction scope (eventual consistency, Outbox processing).

            var customer = await this._customerRepository.GetByIdAsync(notification.CustomerId);

            // Send welcome e-mail message...

            customer.MarkAsWelcomedByEmail();
        }
    }
}