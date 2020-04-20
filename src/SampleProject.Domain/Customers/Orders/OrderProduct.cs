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
            ProductPriceData productPrice,
            int quantity,
            string currency,
            List<ConversionRate> conversionRates)
        {
            this.ProductId = productPrice.ProductId;
            this.Quantity = quantity;

            this.CalculateValue(productPrice, currency, conversionRates);
        }

        internal static OrderProduct CreateForProduct(
            ProductPriceData productPrice, int quantity, string currency,
            List<ConversionRate> conversionRates)
        {
            return new OrderProduct(productPrice, quantity, currency, conversionRates);
        }

        internal void ChangeQuantity(ProductPriceData productPrice, int quantity, List<ConversionRate> conversionRates)
        {
            this.Quantity = quantity;

            this.CalculateValue(productPrice, this.Value.Currency, conversionRates);
        }

        private void CalculateValue(ProductPriceData productPrice, string currency, List<ConversionRate> conversionRates)
        {
            this.Value = this.Quantity * productPrice.Price;
            if (currency == "EUR")
            {
                this.ValueInEUR = this.Quantity * productPrice.Price;
            }
            else
            {
                var conversionRate = conversionRates.Single(x => x.SourceCurrency == currency && x.TargetCurrency == "EUR");
                this.ValueInEUR = conversionRate.Convert(this.Value);
            }
        }
    }
}