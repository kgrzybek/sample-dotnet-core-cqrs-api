using Newtonsoft.Json;
using SampleProject.Application.Configuration.DomainEvents;
using SampleProject.Domain.Payments;

namespace SampleProject.Application.Payments
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