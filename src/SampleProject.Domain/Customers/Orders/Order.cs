using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders
{
    public class Order : Entity
    {
        internal Guid Id;
        private bool _isRemoved;
        private MoneyValue _value;
        private MoneyValue _valueInEUR;
        private List<OrderProduct> _orderProducts;
        private OrderStatus _status;

        private Order()
        {
            this._orderProducts = new List<OrderProduct>();
            this._isRemoved = false;
        }

        public Order(
            List<OrderProduct> orderProducts)
        {
            this.Id = Guid.NewGuid();
            this._orderProducts = orderProducts;

            this.CalculateOrderValue();
            this._status = OrderStatus.Placed;
        }

        internal void Change(
            List<OrderProduct> orderProducts, 
            List<ConversionRate> conversionRates)
        {
            foreach (var orderProduct in orderProducts)
            {
                var existingOrderProduct = this._orderProducts.SingleOrDefault(x => x.Product == orderProduct.Product);
                if (existingOrderProduct != null)
                {
                    existingOrderProduct.ChangeQuantity(orderProduct.Quantity, conversionRates);
                }
                else
                {
                    this._orderProducts.Add(orderProduct);
                }
            }

            var existingProducts = this._orderProducts.ToList();
            foreach (var existingProduct in existingProducts)
            {
                var product = orderProducts.SingleOrDefault(x => x.Product == existingProduct.Product);
                if (product == null)
                {
                    this._orderProducts.Remove(existingProduct);
                }
            }

            this.CalculateOrderValue();
        }

        internal void Remove()
        {
            this._isRemoved = true;
        }

        private void CalculateOrderValue()
        {
            var value = this._orderProducts.Sum(x => x.Value.Value);
            this._value = new MoneyValue(value, this._orderProducts.First().Value.Currency);

            var valueInEUR = this._orderProducts.Sum(x => x.ValueInEUR.Value);            
            this._valueInEUR = new MoneyValue(valueInEUR, "EUR");
        }
    }
}