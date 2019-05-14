using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.Infrastructure.InternalCommands;

namespace SampleProject.API.Customers.IntegrationHandlers
{
    public class CustomerRegisteredNotificationHandler : INotificationHandler<CustomerRegisteredNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public CustomerRegisteredNotificationHandler(
            ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(CustomerRegisteredNotification notification, CancellationToken cancellationToken)
        {
            // Send welcome e-mail message...

            await this._commandsScheduler.EnqueueAsync(new MarkCustomerAsWelcomedCommand(notification.CustomerId));
        }
    }
}