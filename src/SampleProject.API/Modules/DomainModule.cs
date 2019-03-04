using Autofac;
using SampleProject.API.Customers.DomainServices;
using SampleProject.Domain.Customers;
using SampleProject.Domain.Customers.Orders;
using SampleProject.Domain.Products;
using SampleProject.Infrastructure;
using SampleProject.Infrastructure.Customers;
using SampleProject.Infrastructure.Products;

namespace SampleProject.API.Modules
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();
        }
    }
}