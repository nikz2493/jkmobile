using System.IO;
using System.Threading;
using RazorEngine.Templating;
using Utility.Logger;
using System.Web.Hosting;
using System;

namespace Utility.Email
{
    /// <summary>
    /// Class Name      : EmailConfiguration
    /// Author          : Ranjana Singh
    /// Creation Date   : 04 Jan 2018
    /// Purpose         : Email Configuration
    /// Revision        : 
    /// </summary>
    public class EmailConfiguration : IEmailConfiguration
    {
        public string MailTemplatePath { set; get; }
        public bool Usethread { set; get; }
        private readonly ILogger logger;

        private readonly ISmtpConfiguration smtpConfiguration;
        public EmailConfiguration(ISmtpConfiguration smtpConfiguration, ILogger logger)
        {
            this.smtpConfiguration = smtpConfiguration;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : SendMail
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : Passing mail details 
        /// Revision        : 
        /// </summary>
        /// <param name="model">Template pbject</param>
        /// <param name="numberOfTimeToTry">Number of time to try for sending mail</param>
        /// <param name="attachments">attach file</param>
        /// <returns></returns>
        public bool SendMail(DynamicViewBag model, int numberOfTimeToTry = 1, string[] attachments = null)
        {
            try
            {
                var templateService = RazorEngineService.Create();
                string applicationPath = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                string path = Path.Combine(applicationPath, MailTemplatePath);
                string mailBody = templateService.RunCompile(File.ReadAllText(path), string.Empty, null, model);

                smtpConfiguration.AddMessage(smtpConfiguration.MailSubject, mailBody, attachments);

                return smtpConfiguration.TrySendMail(numberOfTimeToTry);
            }
            catch (Exception ex)
            {
                logger.Error("logEmailException", ex.InnerException);
                return false;
            }
        }

        /// <summary>
        /// Method Name     : MailConfiguration
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : 
        /// Revision        : 
        /// </summary>
        /// <param name="toRecipients"></param>
        /// <param name="ccRecipients"></param>
        /// <param name="bccRecipients"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailTemplatePath"></param>
        public void MailConfiguration(string toRecipients, string ccRecipients, string bccRecipients, string mailSubject, string mailTemplatePath)
        {
            smtpConfiguration.BccRecipients = bccRecipients;
            smtpConfiguration.CcRecipients = ccRecipients;
            smtpConfiguration.ToRecipients = toRecipients;
            smtpConfiguration.MailSubject = mailSubject;
            this.MailTemplatePath = mailTemplatePath;
        }
    }
}
