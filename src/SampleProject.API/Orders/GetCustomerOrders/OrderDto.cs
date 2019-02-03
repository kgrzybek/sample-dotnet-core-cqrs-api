using System;

namespace SampleProject.API.Orders.GetCustomerOrders
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public decimal Value { get; set; }

        public bool IsRemoved { get; set; }
    }
}