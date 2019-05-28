using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}