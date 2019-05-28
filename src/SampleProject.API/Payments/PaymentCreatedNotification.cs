using System;
using Newtonsoft.Json;
using SampleProject.Domain.Payments;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.API.Payments
{
    public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
    {
        public PaymentId PaymentId { get; }

        public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
        {
            this.PaymentId = domainEvent.PaymentId;
        }

        [JsonConstructor]
        public PaymentCreatedNotification(PaymentId paymentId) : base(null)
        {
            this.PaymentId = paymentId;
        }
    }
}