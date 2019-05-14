using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SampleProject.API.Payments.SendEmailAfterPayment;
using SampleProject.Infrastructure.InternalCommands;

namespace SampleProject.API.Payments
{
    public class PaymentCreatedNotificationHandler : INotificationHandler<PaymentCreatedNotification>
    {
        private readonly ICommandsScheduler _commandsScheduler;

        public PaymentCreatedNotificationHandler(ICommandsScheduler commandsScheduler)
        {
            _commandsScheduler = commandsScheduler;
        }

        public async Task Handle(PaymentCreatedNotification request, CancellationToken cancellationToken)
        {
            await _commandsScheduler.EnqueueAsync(new SendEmailAfterPaymentCommand(request.PaymentId));
        }
    }
}