using RazorEngine.Templating;

namespace Utility.Email
{
    public interface IEmailConfiguration
    {
        bool Usethread { set; get; }
        bool SendMail(DynamicViewBag model, int numberOfTimeToTry = 1, string[] attachments = null);
        void MailConfiguration(string toRecipients, string ccRecipients, string bccRecipients, string mailSubject, string mailTemplatePath);
    }
}
