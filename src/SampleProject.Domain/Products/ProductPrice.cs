using SampleProject.Domain.Customers.Orders;

namespace SampleProject.Domain.Products
{
    public class ProductPrice
    {
        public MoneyValue Value { get; private set; }

        private ProductPrice()
        {
            
        }
    }
}