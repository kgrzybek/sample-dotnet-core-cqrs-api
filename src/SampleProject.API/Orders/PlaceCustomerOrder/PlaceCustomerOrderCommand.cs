using System;
using System.Collections.Generic;
using MediatR;

namespace SampleProject.API.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public List<ProductDto> Products { get; }

        public PlaceCustomerOrderCommand(
            Guid customerId, 
            List<ProductDto> products)
        {
            this.CustomerId = customerId;
            this.Products = products;
        }
    }
}