using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;
using SampleProject.Domain.SharedKernel;

namespace SampleProject.Domain.Customers.Orders
{
    public class Order : Entity
    {
        internal OrderId Id;
        private bool _isRemoved;
        private MoneyValue _value;
        private MoneyValue _valueInEUR;
        private List<OrderProduct> _orderProducts;
        private OrderStatus _status;
        private DateTime _orderDate;
        private DateTime? _orderChangeDate;

        private Order()
        {
            this._orderProducts = new List<OrderProduct>();
            this._isRemoved = false;
        }

        public Order(
            List<OrderProduct> orderProducts)
        {
            this._orderDate = DateTime.UtcNow;
            this.Id = new OrderId(Guid.NewGuid());
            this._orderProducts = orderProducts;

            this.CalculateOrderValue();
            this._status = OrderStatus.Placed;
        }

        internal void Change(
            List<Product> existingProducts,
            List<OrderProduct> orderProducts, 
            List<ConversionRate> conversionRates)
        {
            foreach (var orderProduct in orderProducts)
            {
                var existingProduct = existingProducts.SingleOrDefault(x => x.Id == orderProduct.ProductId);
                if (existingProduct != null)
                {
                    var existingOrderProduct = this._orderProducts.Single(x => x.ProductId == existingProduct.Id);
                    existingOrderProduct.ChangeQuantity(existingProduct, orderProduct.Quantity, conversionRates);
                }
                else
                {
                    this._orderProducts.Add(orderProduct);
                }
            }

            foreach (var existingProduct in existingProducts)
            {
                var product = orderProducts.SingleOrDefault(x => x.ProductId == existingProduct.Id);
                if (product == null)
                {
                    var existingOrderProduct = this._orderProducts.Single(x => x.ProductId == existingProduct.Id);
                    this._orderProducts.Remove(existingOrderProduct);
                }
            }

            this.CalculateOrderValue();

            this._orderChangeDate = DateTime.UtcNow;
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

        internal bool IsOrderedToday()
        {
           return this._orderDate.Date == DateTime.UtcNow.Date;
        }

        public List<ProductId> GetProductsIds()
        {
            return this._orderProducts.Select(x => x.ProductId).ToList();
        }
    }
}