using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Infrastructure.Customers;

namespace SampleProject.API.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        public Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            // Send event to bus or e-mail message...

            return Task.CompletedTask;
        }
    }
}