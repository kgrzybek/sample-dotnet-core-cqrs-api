using MediatR;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Application.Configuration.Emails;
using SampleProject.Domain.Payments;
using System.Threading;
using System.Threading.Tasks;

namespace SampleProject.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommandHandler : ICommandHandler<SendEmailAfterPaymentCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IPaymentRepository _paymentRepository;

        public SendEmailAfterPaymentCommandHandler(
            IEmailSender emailSender,
            IPaymentRepository paymentRepository)
        {
            _emailSender = emailSender;
            _paymentRepository = paymentRepository;
        }

        public async Task<Unit> Handle(SendEmailAfterPaymentCommand request, CancellationToken cancellationToken)
        {
            // Logic of preparing an email. This is only mock.
            EmailMessage emailMessage = new EmailMessage("from@email.com", "to@email.com", "content");

            await _emailSender.SendEmailAsync(emailMessage);

            Payment payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            payment.MarkEmailNotificationIsSent();

            return Unit.Value;
        }
    }
}