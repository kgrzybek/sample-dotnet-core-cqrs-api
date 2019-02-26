using SampleProject.Infrastructure.Caching;

namespace SampleProject.Infrastructure.ForeignExchange
{
    public class ConversionRatesCacheKey : ICacheKey<ConversionRatesCache>
    {
        public string CacheKey => "ConversionRatesCache";
    }
}