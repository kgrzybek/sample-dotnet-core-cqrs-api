using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments
{
    public class PaymentCreatedEvent : DomainEventBase
    {
        public PaymentCreatedEvent(Guid paymentId, Guid orderId)
        {
            this.PaymentId = paymentId;
            this.OrderId = orderId;
        }

        public Guid PaymentId { get; }

        public Guid OrderId { get; }
    }
}