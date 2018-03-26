using System.Collections.Generic;

namespace JKMServices.BLL.EmailEngine
{
    public interface IEmailTemplate
    {
        bool Send<T>(List<T> obj, int numberOfTimeToTry = 1, string[] attachments = null);
        void MailConfiguration(string customerId, string templateCode);
    }
}
