using System;
using Newtonsoft.Json;
using SampleProject.Domain.Payments;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class PaymentCreatedNotification : DomainNotificationBase<PaymentCreatedEvent>
    {
        public Guid PaymentId { get; }

        public PaymentCreatedNotification(PaymentCreatedEvent domainEvent) : base(domainEvent)
        {
            this.PaymentId = domainEvent.PaymentId;
        }

        [JsonConstructor]
        public PaymentCreatedNotification(Guid paymentId) : base(null)
        {
            this.PaymentId = paymentId;
        }
    }
}