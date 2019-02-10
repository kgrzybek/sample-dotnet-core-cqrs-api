using SampleProject.Domain.Products;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Domain.Customers.Orders
{
    public class OrderProduct : Entity
    {
        public int Quantity { get; private set; }

        public Product Product { get; private set; }

        internal MoneyValue Value { get; private set; }

        private OrderProduct()
        {

        }

        public OrderProduct(
            Product product, 
            int quantity, 
            string currency)
        {
            this.Product = product;
            this.Quantity = quantity;

            var totalValueForOrderProduct = this.Quantity * this.Product.GetPrice(currency).Value;
            this.Value = new MoneyValue(totalValueForOrderProduct, currency);
        }

        internal void ChangeQuantity(int quantity)
        {
            this.Quantity = quantity;
        }
    }
}