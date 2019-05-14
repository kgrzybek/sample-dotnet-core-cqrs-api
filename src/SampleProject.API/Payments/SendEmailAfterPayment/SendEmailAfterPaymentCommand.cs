using System;
using MediatR;

namespace SampleProject.API.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : IRequest
    {
        public Guid PaymentId { get; }

        public SendEmailAfterPaymentCommand(Guid paymentId)
        {
            this.PaymentId = paymentId;
        }
    }
}