using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class Product : Entity
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public MoneyValue Price { get; private set; }

        private Product()
        {

        }

        public Product(string name)
        {
            this.Name = name;
        }
    }
}