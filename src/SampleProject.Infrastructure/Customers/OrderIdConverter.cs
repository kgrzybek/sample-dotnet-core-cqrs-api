using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class OrderIdConverter : TypedIdValueConverterBase<OrderId>
    {
        public OrderIdConverter(ConverterMappingHints mappingHints = null) : base(mappingHints)
        {
            
        }
    }
}