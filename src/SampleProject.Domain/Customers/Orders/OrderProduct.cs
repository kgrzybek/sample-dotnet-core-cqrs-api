using System;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderProduct : Entity
    {
        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public Product Product { get; private set; }

        private OrderProduct()
        {

        }

        public OrderProduct(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        internal void ChangeQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}