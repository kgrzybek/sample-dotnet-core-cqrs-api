using SampleProject.Domain.SeedWork;
using System.Collections.Generic;

namespace SampleProject.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private readonly List<ProductPrice> _prices;

        private Product()
        {

        }
    }
}