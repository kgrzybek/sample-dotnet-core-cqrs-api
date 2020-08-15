using MediatR;
using Newtonsoft.Json;
using SampleProject.Application.Configuration.Commands;
using SampleProject.Domain.Payments;
using System;

namespace SampleProject.Application.Payments.SendEmailAfterPayment
{
    public class SendEmailAfterPaymentCommand : InternalCommandBase<Unit>
    {
        public PaymentId PaymentId { get; }

        [JsonConstructor]
        public SendEmailAfterPaymentCommand(Guid id, PaymentId paymentId) : base(id)
        {
            PaymentId = paymentId;
        }
    }
}