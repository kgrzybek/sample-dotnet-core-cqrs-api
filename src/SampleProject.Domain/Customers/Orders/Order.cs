using System;
using System.Collections.Generic;
using System.Linq;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class Order : Entity
    {
        public Guid Id { get; private set; }
        private bool _isRemoved;
        private decimal _value;
        private List<OrderProduct> _orderProducts;

        private Order()
        {
            this._orderProducts = new List<OrderProduct>();
            this._isRemoved = false;
        }

        public Order(List<OrderProduct> orderProducts, IReadOnlyCollection<Product> allProducts)
        {
            this.Id = Guid.NewGuid();
            this._orderProducts = orderProducts;

            this.CalculateOrderValue(allProducts);
        }

        internal void Change(List<OrderProduct> products, IReadOnlyCollection<Product> allProducts)
        {
            foreach (var product in products)
            {
                var orderProduct = this._orderProducts.SingleOrDefault(x => x.ProductId == product.ProductId);
                if (orderProduct != null)
                {
                    orderProduct.ChangeQuantity(product.Quantity);
                }
                else
                {
                    this._orderProducts.Add(product);
                }
            }

            var existingProducts = this._orderProducts.ToList();
            foreach (var existingProduct in existingProducts)
            {
                var product = products.SingleOrDefault(x => x.ProductId == existingProduct.ProductId);
                if (product == null)
                {
                    this._orderProducts.Remove(existingProduct);
                }
            }

            this.CalculateOrderValue(allProducts);
        }

        internal void Remove()
        {
            this._isRemoved = true;
        }

        private void CalculateOrderValue(IReadOnlyCollection<Product> allProducts)
        {
            this._value = this._orderProducts.Sum(x => x.Quantity * allProducts.Single(y => y.Id == x.ProductId).Price);
        }
    }
}