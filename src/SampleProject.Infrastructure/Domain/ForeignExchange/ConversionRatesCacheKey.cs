using SampleProject.Infrastructure.Caching;

namespace SampleProject.Infrastructure.Domain.ForeignExchange
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}