using SampleProject.Domain.ForeignExchange;
using SampleProject.Infrastructure.Caching;
using System;
using System.Collections.Generic;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges
{
    public class ForeignExchange : IForeignExchange
    {
        private readonly ICacheStore _cacheStore;

        public ForeignExchange(ICacheStore cacheStore)
        {
            _cacheStore = cacheStore;
        }

        public List<ConversionRate> GetConversionRates()
        {
            ConversionRatesCache ratesCache = _cacheStore.Get(new ConversionRatesCacheKey());

            if (ratesCache != null)
            {
                return ratesCache.Rates;
            }

            List<ConversionRate> rates = GetConversionRatesFromExternalApi();

            _cacheStore.Add(new ConversionRatesCache(rates), new ConversionRatesCacheKey(), DateTime.Now.Date.AddDays(1));

            return rates;
        }

        private static List<ConversionRate> GetConversionRatesFromExternalApi()
        {
            // Communication with external API. Here is only mock.

            List<ConversionRate> conversionRates = new List<ConversionRate>
            {
                new ConversionRate("USD", "EUR", (decimal)0.88),
                new ConversionRate("EUR", "USD", (decimal)1.13)
            };

            return conversionRates;
        }
    }
}