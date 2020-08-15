using System;

namespace SampleProject.Application.Orders
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }



        public string Name { get; set; }

        public ProductDto()
        {

        }

        public ProductDto(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }
    }
}