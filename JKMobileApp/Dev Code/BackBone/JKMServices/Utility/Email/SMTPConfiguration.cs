using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Utility.Logger;

namespace Utility.Email
{
    /// <summary>
    /// Class Name      : SMTPConfiguration
    /// Author          : Ranjana Singh
    /// Creation Date   : 04 Jan 2018
    /// Purpose         : It's a abstract class to configuration email     
    /// Revision        : 
    /// </summary>
    public class SmtpConfiguration : ISmtpConfiguration
    {
        public string ToRecipients { set; get; }
        public string BccRecipients { set; get; }
        public string CcRecipients { set; get; }
        public string MailSubject { set; get; }

        private readonly ISmtpClientFactory smtpClientFactory;
        private readonly MailMessage emailMessage;
        private readonly IRetry retry;

        public SmtpConfiguration(ISmtpClientFactory smtpClientFactory, MailMessage emailMessage, IRetry retry)
        {
            this.smtpClientFactory = smtpClientFactory;
            this.emailMessage = emailMessage;
            this.retry = retry;
            ConfigureSMTPSettings();
        }

        /// <summary>
        /// Method Name     : ConfigureSMTPSettings
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : Configure SMTP setting 
        /// Revision        : 
        /// </summary>
        private void ConfigureSMTPSettings()
        {
            smtpClientFactory.SmtpClient.Host = General.GetConfigValue("smtpHost");
            smtpClientFactory.SmtpClient.Port = Convert.ToInt32(General.GetConfigValue("smtpPort"));
            smtpClientFactory.SmtpClient.Credentials = new NetworkCredential(General.GetConfigValue("smtpUser"),
                (General.GetConfigValue("smtpPassword")));
            smtpClientFactory.SmtpClient.EnableSsl = Convert.ToBoolean(General.GetConfigValue("isSSL"));
        }

        /// <summary>
        /// Method Name     : AddMessage
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : Add Mail Message with attactment file
        /// Revision        : 
        /// </summary>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="attachments"></param>
        public void AddMessage(string mailSubject, string mailBody, string[] attachments = null)
        {
            string logoPath, logoFullPath, imagePath, imageFullPath, applicationPath = string.Empty;

            //Add recipients to the mail object
            AddRecipients();

            emailMessage.From = new MailAddress(General.GetConfigValue("sender"));
            emailMessage.Sender = new MailAddress(General.GetConfigValue("sender"));
            emailMessage.Subject = mailSubject;
            emailMessage.IsBodyHtml = true;

            applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

            logoPath = General.GetConfigValue("JKMOVING_LOGO");
            logoFullPath = Path.Combine(applicationPath, logoPath);

            imagePath = General.GetConfigValue("JKMOVING_Image");
            imageFullPath = Path.Combine(applicationPath, imagePath);

            AlternateView logoView = AlternateView.CreateAlternateViewFromString(mailBody, null, MediaTypeNames.Text.Html);
            LinkedResource logoImage = new LinkedResource(logoFullPath, "image/png")
            {
                TransferEncoding = System.Net.Mime.TransferEncoding.Base64,
                ContentId = "logoID"
            };
            logoView.LinkedResources.Add(logoImage);

            LinkedResource emailImage = new LinkedResource(imageFullPath, "image/png")
            {
                ContentId = "ImageID",
                TransferEncoding = System.Net.Mime.TransferEncoding.Base64
            };
            logoView.LinkedResources.Add(emailImage);

            emailMessage.AlternateViews.Add(logoView);

            if (attachments != null)
            {
                foreach (string strattach in attachments)
                {
                    emailMessage.Attachments.Add(new Attachment(strattach));
                }
            }
        }

        /// <summary>
        /// Method Name     : AddRecipients
        /// Author          : Jitendra Garg
        /// Creation Date   : 15 Jan 2018
        /// Purpose         : Adds recipients to the mail object (Reduces cognitive complexity of AddMessage function 
        /// Revision        :
        /// </summary>
        private void AddRecipients()
        {
            string[] mailAddress = null;

            if (!string.IsNullOrEmpty(ToRecipients))
            {
                mailAddress = ToRecipients.Split(',');
                foreach (string strRecp in mailAddress)
                {
                    emailMessage.To.Add(new MailAddress(strRecp));
                }
            }
            if (!string.IsNullOrEmpty(BccRecipients))
            {
                mailAddress = BccRecipients.Split(',');
                foreach (string strRecp in mailAddress)
                {
                    emailMessage.Bcc.Add(new MailAddress(strRecp));
                }
            }

            if (!string.IsNullOrEmpty(CcRecipients))
            {
                mailAddress = CcRecipients.Split(',');
                foreach (string strRecp in mailAddress)
                {
                    emailMessage.CC.Add(new MailAddress(strRecp));
                }
            }
        }

        /// <summary>
        /// Method Name     : TrySendMail
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : Send mail 
        /// Revision        :
        /// </summary>
        /// <param name="numberOfTimeToTry"></param>
        /// <returns></returns>
        public bool TrySendMail(int numberOfTimeToTry = 1)
        {
            return retry.Do(() => smtpClientFactory.Send(emailMessage), TimeSpan.FromSeconds(5), numberOfTimeToTry);
        }
    }
}
