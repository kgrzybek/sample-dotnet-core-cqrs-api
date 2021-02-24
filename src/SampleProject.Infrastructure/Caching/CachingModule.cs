using Autofac;
using System;
using System.Collections.Generic;

namespace SampleProject.Infrastructure.Caching
{
    public class CachingModule : Module
    {
        private readonly Dictionary<string, TimeSpan> _expirationConfiguration;

        public CachingModule(Dictionary<string, TimeSpan> expirationConfiguration)
        {
            _expirationConfiguration = expirationConfiguration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemoryCacheStore>()
                .As<ICacheStore>()
                .WithParameter("expirationConfiguration", _expirationConfiguration)
                .SingleInstance();
        }
    }
}