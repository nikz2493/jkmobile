using System.Collections.Generic;

namespace JKMServices.BLL.EmailEngine
{
    public interface IEmailTemplateFactory
    {
        void TemplateConfiguration(string templateName, string customerId, string templateCode);
        bool Send<T>(List<T> obj, int numberOfTimeToTry = 1, string[] attachments = null);
    }
}
