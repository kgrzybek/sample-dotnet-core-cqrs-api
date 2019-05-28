using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Payments
{
    public class PaymentId : TypedIdValueBase
    {
        public PaymentId(Guid value) : base(value)
        {
        }
    }
}