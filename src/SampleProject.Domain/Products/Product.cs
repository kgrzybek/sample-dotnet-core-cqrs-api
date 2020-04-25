using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }
    }
}