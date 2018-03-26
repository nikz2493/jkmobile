using System.Net.Mail;
using Utility.Logger;

namespace Utility.Email
{
    public class SmtpClientFactory : ISmtpClientFactory
    {
        private readonly System.Net.Mail.SmtpClient smtpClient;
        private readonly ILogger logger;

        public SmtpClientFactory(System.Net.Mail.SmtpClient smtpClient, ILogger logger=null)
        { 
            this.smtpClient=smtpClient;
            this.logger = logger;
        }

        public System.Net.Mail.SmtpClient SmtpClient { get { return smtpClient; } }

        public bool Send(MailMessage mailMessage)
        {
            smtpClient.Send(mailMessage);
            return true;
        }
    }
}
