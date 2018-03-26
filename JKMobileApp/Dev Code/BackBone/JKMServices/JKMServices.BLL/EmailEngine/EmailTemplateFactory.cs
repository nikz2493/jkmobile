using System;
using System.Collections.Generic;
using Ninject;
using Utility.Logger;

namespace JKMServices.BLL.EmailEngine
{
    public class EmailTemplateFactory : IEmailTemplateFactory
    {
        private IEmailTemplate emailTemplate;
        public IEmailTemplate EmailTemplate { get { return emailTemplate; } }
        private readonly Logger logger = new Logger(new LoggerStackTrace());
           
        public void TemplateConfiguration(string templateName, string customerId, string templateCode)
        {
            logger.Info("TemplateConfiguration Encountered");
            Type type = this.GetType().Assembly.GetType(templateName);
            var customerIdParam = new Ninject.Parameters.ConstructorArgument("customerId", customerId);
            var templateCodeParam = new Ninject.Parameters.ConstructorArgument("templateCode", templateCode);
            emailTemplate = Utility.General.Resolve<IEmailTemplate>(type, customerIdParam, templateCodeParam);
        }

        public bool Send<T>(List<T> obj, int numberOfTimeToTry = 1, string[] attachments = null)
        {
            return EmailTemplate.Send<T>(obj, numberOfTimeToTry, attachments);
        }
    }
}
