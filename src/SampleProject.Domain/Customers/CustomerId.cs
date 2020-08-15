using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}