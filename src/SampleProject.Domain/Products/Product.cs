using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }

        internal MoneyValue GetPrice(string currency)
        {
            return this._prices.Single(x => x.Value.Currency == currency).Value;
        }
    }
}