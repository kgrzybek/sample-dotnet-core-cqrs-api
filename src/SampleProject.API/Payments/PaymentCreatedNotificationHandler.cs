using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SampleProject.Infrastructure.Customers
{
    public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
    {
        public Task Handle(PaymentCreatedNotification notification, CancellationToken cancellationToken)
        {
            // Send e-mail about payment logic. This is executed outside transaction boundary by Outbox processing.

            return Task.CompletedTask;
        }
    }
}