using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Customers;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class CustomerIdConverter : TypedIdValueConverterBase<CustomerId>
    {
        public CustomerIdConverter(ConverterMappingHints mappingHints = null) : base(mappingHints)
        {
            
        }
    }
}