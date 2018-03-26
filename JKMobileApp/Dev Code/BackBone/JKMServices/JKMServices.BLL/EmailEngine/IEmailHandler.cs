using System;

namespace JKMServices.BLL.EmailEngine
{
    public interface IEmailHandler
    {
        void Add<T>(string userId, string emailTemplateCode, T emailObject, string[] attachmentfile = null);
        void Send(int numberOfTimeToTry = 1);
    }
}
