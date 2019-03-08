using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Quartz;
using SampleProject.API.Outbox;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Caching;
using Module = Autofac.Module;

namespace SampleProject.API.Modules
{
    public class OutboxModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(x => typeof(IJob).IsAssignableFrom(x)).InstancePerDependency();
        }
    }
}