using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CommonServiceLocator;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SampleProject.Infrastructure.SeedWork
{
    /// <summary>
    /// Based on https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/
    /// </summary>
    public class StronglyTypedIdValueConverterSelector : ValueConverterSelector
    {
        private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters
            = new ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo>();

        private readonly IList<ITypedIdValueConverter> _valueConverters;

        public StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies) : base(
            dependencies)
        {
            _valueConverters = ServiceLocator.Current.GetAllInstances<ITypedIdValueConverter>().ToList();
        }

        public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type providerClrType = null)
        {
            var baseConverters = base.Select(modelClrType, providerClrType);
            foreach (var converter in baseConverters)
            {
                yield return converter;
            }

            var underlyingModelType = UnwrapNullableType(modelClrType);
            var underlyingProviderType = UnwrapNullableType(providerClrType);

            if (underlyingProviderType is null || underlyingProviderType == typeof(Guid))
            {
                Type converterType = null;

                var converter =
                    _valueConverters.SingleOrDefault(x => x.ConverterFor == underlyingModelType);

                if (converter != null)
                {
                    converterType = converter.GetType();
                }

                if (converterType != null)
                {
                    yield return _converters.GetOrAdd(
                        (underlyingModelType, typeof(Guid)),
                        k =>
                        {
                            ValueConverter Factory(ValueConverterInfo info) =>
                                (ValueConverter) Activator.CreateInstance(converterType, info.MappingHints);

                            return new ValueConverterInfo(modelClrType, typeof(Guid), Factory);
                        }
                    );
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