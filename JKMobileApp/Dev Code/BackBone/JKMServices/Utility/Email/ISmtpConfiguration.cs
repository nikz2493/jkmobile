namespace Utility.Email
{
    public interface ISmtpConfiguration
    {
        string ToRecipients { set; get; }
        string BccRecipients { set; get; }
        string CcRecipients { set; get; }
        string MailSubject { set; get; }

        void AddMessage(string mailSubject, string mailBody, string[] attachments = null);
        bool TrySendMail(int numberOfTimeToTry = 1);
    }
}
