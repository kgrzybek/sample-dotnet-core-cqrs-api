using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Payments;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Customers
{
    public class PaymentIdConverter : TypedIdValueConverterBase<PaymentId>
    {
        public PaymentIdConverter(ConverterMappingHints mappingHints = null) : base(mappingHints)
        {
            
        }
    }
}