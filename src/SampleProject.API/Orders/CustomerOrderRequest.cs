using System.Collections.Generic;
using SampleProject.Application.Orders;

namespace SampleProject.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<ProductDto> Products { get; set; }

        public string Currency { get; set; }
    }
}