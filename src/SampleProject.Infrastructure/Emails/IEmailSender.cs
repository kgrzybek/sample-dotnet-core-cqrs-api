using System.Threading.Tasks;

namespace SampleProject.Infrastructure.Emails
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailMessage message);
    }
}