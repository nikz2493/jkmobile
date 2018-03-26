using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Logger;

namespace JKMServices.BLL.EmailEngine
{
    /// <summary>
    /// Class Name      : EmailHandler
    /// Author          : Ranjana Singh
    /// Creation Date   : 04 Jan 2018
    /// Purpose         : Collecting Mail information 
    /// Revision        : 
    /// </summary>
    public class EmailHandler : IEmailHandler
    {
        public class ManageMailObject
        {
            public string UserId { set; get; }
            public string TemplateCode { set; get; }
            public object EmailObject { set; get; }
            public string[] AttachmentFile { set; get; }
            public string Message { set; get; }
            public Exception Exception { set; get; }
        }

        private class MailKeyObject
        {
            public string UserId { set; get; }
            public string TemplateName { set; get; }
            public string TemplateCode { set; get; }
        }

        private readonly Dictionary<MailKeyObject, List<ManageMailObject>> manageEmailObjectList
            = new Dictionary<MailKeyObject, List<ManageMailObject>>();

        private readonly IEmailTemplateFactory factory;

        public EmailHandler(IEmailTemplateFactory factory)
        {
            this.factory = factory;
        }

        public void Add<T>(string userId, string emailTemplateCode, T emailObject, string[] attachmentfile = null)
        {
            var manageEmailObject = new ManageMailObject()
            {
                UserId = userId,
                TemplateCode = emailTemplateCode,
                EmailObject = emailObject,
                AttachmentFile = attachmentfile
            };
            SetupDictionary(manageEmailObject);
        }

        public void Send(int numberOfTimeToTry = 1)
        {
            foreach (KeyValuePair<MailKeyObject, List<ManageMailObject>> emailObject in manageEmailObjectList)
            {
                var attachmentFile = emailObject.Value.Select(x => x.AttachmentFile).ToList();
                string[] attactment = GetFile(attachmentFile);
                factory.TemplateConfiguration(emailObject.Key.TemplateName, emailObject.Key.UserId, emailObject.Key.TemplateCode);
                factory.Send<ManageMailObject>(emailObject.Value, numberOfTimeToTry, attactment);
            }
        }

        private string[] GetFile(dynamic attachmentFile)
        {
            List<string> fileList = new List<string>();
            for (int i = 0; i < ((List<string[]>)attachmentFile).Count; i++)
            {
                if (attachmentFile[i] != null)
                    fileList.AddRange(attachmentFile[i]);
            }
            fileList = fileList.Distinct().ToList();
            return fileList.ToArray();
        }

        private void SetupDictionary(ManageMailObject manageEmailObject)
        {
            MailKeyObject keyObject = new MailKeyObject
            {
                UserId = manageEmailObject.UserId,
                TemplateCode = manageEmailObject.TemplateCode,
                TemplateName = Utility.General.GetConfigValue(manageEmailObject.TemplateCode + "_TemplateName")
            };
            var manageEmailList = new List<ManageMailObject>
            {
                manageEmailObject
            };
            manageEmailObjectList.Add(keyObject, manageEmailList);
        }
    }
}
