using MediatR;
using SampleProject.Domain.Payments;

namespace SampleProject.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : IRequest
    {
        public PaymentId PaymentId { get; }

        public SendEmailAfterPaymentCommand(PaymentId paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}