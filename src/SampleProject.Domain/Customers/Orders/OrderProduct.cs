using System;
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

        public ProductId ProductId { get; private set; }

        internal MoneyValue Value { get; private set; }

        internal MoneyValue ValueInEUR { get; private set; }

        private OrderProduct()
        {

        }

        private OrderProduct(
            Product product, 
            int quantity, 
            string currency,
            List<ConversionRate> conversionRates)
        {
            this.ProductId = product.Id;
            this.Quantity = quantity;

            this.CalculateValue(product, currency, conversionRates);
        }

        internal static OrderProduct CreateForProduct(Product product, int quantity, string currency,
            List<ConversionRate> conversionRates)
        {
            return new OrderProduct(product, quantity, currency, conversionRates);
        }

        internal void ChangeQuantity(Product product, int quantity, List<ConversionRate> conversionRates)
        {
            this.Quantity = quantity;

            this.CalculateValue(product, this.Value.Currency, conversionRates);
        }

        private void CalculateValue(Product product, string currency, List<ConversionRate> conversionRates)
        {
            var totalValueForOrderProduct = this.Quantity * product.GetPrice(currency).Value;
            this.Value = new MoneyValue(totalValueForOrderProduct, currency);

            var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == "EUR");

            this.ValueInEUR = conversionRate.Convert(this.Value);
        }
    }
}