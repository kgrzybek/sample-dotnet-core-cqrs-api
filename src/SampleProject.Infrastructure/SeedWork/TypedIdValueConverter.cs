using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.SeedWork;
using System;

namespace SampleProject.Infrastructure.SeedWork
{
    public class TypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
        where TTypedIdValue : TypedIdValueBase
    {
        public TypedIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        private static TTypedIdValue Create(Guid id)
        {
            return Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
        }
    }
}