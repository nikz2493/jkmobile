using System.Net.Mail;

namespace Utility.Email
{
    public interface ISmtpClientFactory
    {
        System.Net.Mail.SmtpClient SmtpClient { get; }
        bool Send(MailMessage mailMessage);
    }
}
