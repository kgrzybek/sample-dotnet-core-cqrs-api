using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleProject.Domain.SeedWork;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SampleProject.Infrastructure.SeedWork
{
    /// <summary>
    /// Based on https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/
    /// </summary>
    public class StronglyTypedIdValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
            = new ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo>();

        public StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            IEnumerable<ValueConverterInfo> baseConverters = base.Select(modelClrType, providerClrType);
            foreach (ValueConverterInfo converter in baseConverters)
            {
                yield return converter;
            }

            Type underlyingModelType = UnwrapNullableType(modelClrType);
            Type underlyingProviderType = UnwrapNullableType(providerClrType);

            if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
            {
                bool isTypedIdValue = typeof(TypedIdValueBase).IsAssignableFrom(underlyingModelType);
                if (isTypedIdValue)
                {
                    Type converterType = typeof(TypedIdValueConverter<>).MakeGenericType(underlyingModelType);

                    yield return _converters.GetOrAdd((underlyingModelType, typeof(Guid)), _ =>
                    {
                        return new ValueConverterInfo(
                            modelClrType: modelClrType,
                            providerClrType: typeof(Guid),
                            factory: valueConverterInfo => (ValueConverter)Activator.CreateInstance(converterType, valueConverterInfo.MappingHints));
                    });
                }
            }
        }

        private static Type UnwrapNullableType(Type type)
        {
            if (type is null)
            {
                return null;
            }

            return Nullable.GetUnderlyingType(type) ?? type;
        }
    }
}