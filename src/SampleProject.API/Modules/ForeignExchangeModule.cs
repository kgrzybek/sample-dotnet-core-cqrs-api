using Autofac;
using SampleProject.Domain.ForeignExchange;
using SampleProject.Infrastructure.ForeignExchange;

namespace SampleProject.API.Modules
{
    public class ForeignExchangeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForeignExchange>()
                .As<IForeignExchange>()
                .InstancePerLifetimeScope();
        }
    }
}