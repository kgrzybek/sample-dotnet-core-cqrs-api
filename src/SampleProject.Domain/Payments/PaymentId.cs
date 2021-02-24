using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Domain.Payments
{
    public class PaymentId : TypedIdValueBase
    {
        public PaymentId(Guid value) : base(value)
        {
        }
    }
}