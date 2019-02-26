using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderProduct : Entity
    {
        public int Quantity { get; private set; }

        public Product Product { get; private set; }

        internal MoneyValue Value { get; private set; }

        internal MoneyValue ValueInEUR { get; private set; }

        private OrderProduct()
        {

        }

        public OrderProduct(
            Product product, 
            int quantity, 
            string currency,
            List<ConversionRate> conversionRates)
        {
            this.Product = product;
            this.Quantity = quantity;

            this.CalculateValue(currency, conversionRates);
        }

        internal void ChangeQuantity(int quantity, List<ConversionRate> conversionRates)
        {
            this.Quantity = quantity;

            this.CalculateValue(this.Value.Currency, conversionRates);
        }

        private void CalculateValue(string currency, List<ConversionRate> conversionRates)
        {
            var totalValueForOrderProduct = this.Quantity * this.Product.GetPrice(currency).Value;
            this.Value = new MoneyValue(totalValueForOrderProduct, currency);

            var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == "EUR");

            this.ValueInEUR = conversionRate.Convert(this.Value);
        }
    }
}