using SampleProject.Application.Configuration.Commands;
using System;
using System.Collections.Generic;

namespace SampleProject.Application.Orders.PlaceCustomerOrder
{
    public class PlaceCustomerOrderCommand : CommandBase<Guid>
    {
        public Guid CustomerId { get; }

        public List<ProductDto> Products { get; }

        public string Currency { get; }

        public PlaceCustomerOrderCommand(
            Guid customerId,
            List<ProductDto> products,
            string currency)
        {
            CustomerId = customerId;
            Products = products;
            Currency = currency;
        }
    }
}