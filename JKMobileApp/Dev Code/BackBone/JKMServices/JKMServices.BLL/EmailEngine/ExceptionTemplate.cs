using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using Utility;
using Utility.Email;
using Utility.Logger;

namespace JKMServices.BLL.EmailEngine
{
    /// <summary>
    /// Class Name      : ExceptionTemplate
    /// Author          : Ranjana Singh
    /// Creation Date   : 04 Jan 2018
    /// Purpose         : Send Exception mail 
    /// Revision        : 
    /// </summary>
    public class ExceptionTemplate : IEmailTemplate
    {        
        private readonly IEmailConfiguration emailConfiguration;

        public ExceptionTemplate(string customerId, string templateCode, IEmailConfiguration emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration;
            MailConfiguration(customerId, templateCode);
        }

        public void MailConfiguration(string customerId, string templateCode)
        {
            string bccRecipients, ccRecipients, toRecipients, mailSubject, mailTemplatePath;

            if (!string.IsNullOrEmpty(General.GetConfigValue(templateCode + "_bccRecipients")))
                bccRecipients = General.GetConfigValue(templateCode + "_bccRecipients");
            else
                bccRecipients = null;

            if (!string.IsNullOrEmpty(General.GetConfigValue(templateCode + "_ccRecipients")))
                ccRecipients = General.GetConfigValue(templateCode + "_ccRecipients");
            else
                ccRecipients = null;

            if (!string.IsNullOrEmpty(General.GetConfigValue(templateCode + "_toRecipients")))
                toRecipients = General.GetConfigValue(templateCode + "_toRecipients");
            else
                toRecipients = customerId;

            if (!string.IsNullOrEmpty(General.GetConfigValue(templateCode + "_Subject")))
                mailSubject = General.GetConfigValue(templateCode + "_Subject");
            else
                mailSubject = null;

            if (!string.IsNullOrEmpty(General.GetConfigValue(templateCode + "_TemplatePath")))
                mailTemplatePath = General.GetConfigValue(templateCode + "_TemplatePath");
            else
                mailTemplatePath = null;

            emailConfiguration.Usethread = false;
            emailConfiguration.MailConfiguration(toRecipients, ccRecipients, bccRecipients, mailSubject, mailTemplatePath);
        }

        /// <summary>
        /// Method Name     : Send
        /// Author          : Ranjana Singh
        /// Creation Date   : 04 Jan 2018
        /// Purpose         : Sending email.//
        /// Revision        : 
        /// </summary>
        public bool Send<T>(List<T> obj, int numberOfTimeToTry = 1, string[] attachments = null)
        {
            bool mailSent = false;
            if (obj.Count > 0)
            {
                dynamic emailTemplateModel = new DynamicViewBag();
                emailTemplateModel.messageList = obj;
                mailSent = emailConfiguration.SendMail(emailTemplateModel, numberOfTimeToTry, attachments);
            }
            return mailSent;
        }
    }
}
