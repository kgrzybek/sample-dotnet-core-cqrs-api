using Autofac;
using SampleProject.Application.Configuration.Emails;
using Module = Autofac.Module;

namespace SampleProject.Infrastructure.Emails
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