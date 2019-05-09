using System;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        private Guid _orderId;

        private DateTime _createDate;

        private PaymentStatus _status;

        private Payment()
        {
            
        }

        public Payment(Guid orderId)
        {
            this.Id = Guid.NewGuid();
            this._createDate = DateTime.UtcNow;
            this._orderId = orderId;
            this._status = PaymentStatus.ToPay;

            this.AddDomainEvent(new PaymentCreatedEvent(this.Id, this._orderId));
        }
    }
}