using SampleProject.Domain.ForeignExchange;
using System.Collections.Generic;

namespace SampleProject.Infrastructure.Domain.ForeignExchanges
{
    public class ConversionRatesCache
    {
        public List<ConversionRate> Rates { get; }

        public ConversionRatesCache(List<ConversionRate> rates)
        {
            Rates = rates;
        }
    }
}