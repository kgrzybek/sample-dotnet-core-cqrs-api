using System;

namespace SampleProject.API.Orders
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }
    }
}