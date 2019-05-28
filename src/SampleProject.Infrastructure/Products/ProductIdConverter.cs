using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.Products;
using SampleProject.Infrastructure.SeedWork;

namespace SampleProject.Infrastructure.Products
{
    public class ProductIdConverter : TypedIdValueConverterBase<ProductId>
    {
        public ProductIdConverter(ConverterMappingHints mappingHints = null) : base(mappingHints)
        {
            
        }
    }
}