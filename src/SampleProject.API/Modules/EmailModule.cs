using Autofac;
using SampleProject.Infrastructure.Emails;
using Module = Autofac.Module;

namespace SampleProject.API.Modules
{
    public class EmailModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailSender>()
                .As<IEmailSender>()
                .InstancePerLifetimeScope();
        }
    }
}