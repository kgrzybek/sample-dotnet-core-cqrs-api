using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}