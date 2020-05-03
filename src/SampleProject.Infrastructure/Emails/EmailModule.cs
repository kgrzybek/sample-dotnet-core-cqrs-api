using Autofac;
using SampleProject.Application.Configuration.Emails;
using Module = Autofac.Module;

namespace SampleProject.Infrastructure.Emails
{
    internal class EmailModule : Module
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsSettings _emailsSettings;
        
        internal EmailModule(IEmailSender emailSender, EmailsSettings emailsSettings)
        {
            _emailSender = emailSender;
            _emailsSettings = emailsSettings;
        }

        internal EmailModule(EmailsSettings emailsSettings)
        {
            _emailsSettings = emailsSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            if (_emailSender != null)
            {
                builder.RegisterInstance(_emailSender);
            }
            else
            {
                builder.RegisterType<EmailSender>()
                    .As<IEmailSender>()
                    .InstancePerLifetimeScope();
            }

            builder.RegisterInstance(_emailsSettings);
        }
    }
}