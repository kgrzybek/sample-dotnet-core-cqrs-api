using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class Product : Entity
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