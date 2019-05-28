using System;

namespace SampleProject.Infrastructure.SeedWork
{
    public interface ITypedIdValueConverter
    {
        Type ConverterFor { get; }
    }
}