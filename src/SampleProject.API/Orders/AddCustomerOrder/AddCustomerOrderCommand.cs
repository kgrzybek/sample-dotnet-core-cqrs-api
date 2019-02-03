using System;
using System.Collections.Generic;
using MediatR;

namespace SampleProject.API.Orders.AddCustomerOrder
{
    public class AddCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public List<ProductDto> Products { get; }

        public AddCustomerOrderCommand(
            Guid customerId, 
            List<ProductDto> products)
        {
            this.CustomerId = customerId;
            this.Products = products;
        }
    }
}