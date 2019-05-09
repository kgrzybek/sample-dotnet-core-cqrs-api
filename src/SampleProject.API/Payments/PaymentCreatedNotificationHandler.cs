using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SampleProject.API.Payments
{
    public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
    {
        public async Task Handle(PaymentCreatedNotification request, CancellationToken cancellationToken)
        {
            // Send e-mail about payment logic. This is executed outside transaction boundary by Outbox processing.
        }
    }
}