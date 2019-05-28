using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.SeedWork;

namespace SampleProject.Infrastructure.SeedWork
{
    public abstract class TypedIdValueConverterBase<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>, ITypedIdValueConverter where TTypedIdValue:TypedIdValueBase
    {
        protected TypedIdValueConverterBase
            (
            ConverterMappingHints mappingHints = null) : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        private static TTypedIdValue Create(Guid guid)
        {
            return Activator.CreateInstance(typeof(TTypedIdValue), guid) as TTypedIdValue;
        }

        public Type ConverterFor => typeof(TTypedIdValue);
    }
}