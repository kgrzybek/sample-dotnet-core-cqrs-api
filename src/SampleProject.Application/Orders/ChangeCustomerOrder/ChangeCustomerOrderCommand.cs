using System;
using System.Collections.Generic;
using MediatR;

namespace SampleProject.Application.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : IRequest
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public List<ProductDto> Products { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId, 
            Guid orderId,
            List<ProductDto> products)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
            this.Products = products;
        }
    }
}