using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}