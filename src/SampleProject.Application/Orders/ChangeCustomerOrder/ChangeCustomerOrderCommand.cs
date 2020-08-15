using MediatR;
using SampleProject.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.ChangeCustomerOrder
{
    public class ChangeCustomerOrderCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public string Currency { get; }

        public List<ProductDto> Products { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId,
            Guid orderId,
            List<ProductDto> products,
            string currency)
        {
            CustomerId = customerId;
            OrderId = orderId;
            Currency = currency;
            Products = products;
        }
    }
}
