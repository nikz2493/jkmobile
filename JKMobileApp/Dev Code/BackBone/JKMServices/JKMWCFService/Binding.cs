using Ninject.Modules;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Binding
    /// Author          : Vivek Bhavsar
    /// Creation Date   : 22 Dec 2017
    /// Purpose         : Bindings for dependency injections using NInject
    /// </summary>
    public class Binding : NinjectModule
    {
        public override void Load()
        {
            // Log
            Bind<Utility.Logger.ILogger>().To<Utility.Logger.Logger>().InSingletonScope();
            Bind<Utility.Logger.ILoggerStackTrace>().To<Utility.Logger.LoggerStackTrace>();

            // Resource Manager
            Bind<JKMServices.BLL.Interface.IResourceManagerFactory>().To<JKMServices.BLL.ResourceManagerFactory>();
            Bind<JKMServices.DAL.IResourceManagerFactory>().To<JKMServices.DAL.ResourceManagerFactory>();

            // BLLs
            Bind<JKMServices.BLL.Interface.ICustomerDetails>().To<JKMServices.BLL.CustomerDetails>();
            Bind<JKMServices.BLL.Interface.IMoveDetails>().To<JKMServices.BLL.MoveDetails>();
            Bind<JKMServices.BLL.Interface.IDocumentDetails>().To<JKMServices.BLL.DocumentDetails>();
            Bind<JKMServices.BLL.Interface.IAlertDetails>().To<JKMServices.BLL.AlertDetails>();
            Bind<JKMServices.BLL.Interface.IEstimateDetails>().To<JKMServices.BLL.EstimateDetails>();
            Bind<JKMServices.BLL.Interface.IPaymentDetails>().To<JKMServices.BLL.PaymentDetails>();

            Bind<JKMServices.BLL.EmailEngine.IEmailHandler>().To<JKMServices.BLL.EmailEngine.EmailHandler>();
            Bind<JKMServices.BLL.EmailEngine.IEmailTemplate>().To<JKMServices.BLL.EmailEngine.ExceptionTemplate>();
            Bind<JKMServices.BLL.EmailEngine.IEmailTemplateFactory>().To<JKMServices.BLL.EmailEngine.EmailTemplateFactory>();

            // DAL > SQLs
            Bind<JKMServices.DAL.SQL.IJKMDBContext>().To<JKMServices.DAL.SQL.JKMDBContext>().InSingletonScope();
            Bind<JKMServices.DAL.CRM.IDocumentDetails>().To<JKMServices.DAL.CRM.DocumentDetails>();
            Bind<JKMServices.DAL.SQL.IAlertDetails>().To<JKMServices.DAL.SQL.AlertDetails>();

            // DAL > CRM > Common
            Bind<JKMServices.DAL.CRM.common.ICRMTODTOMapper>().To<JKMServices.DAL.CRM.common.CRMTODTOMapper>();
            Bind<JKMServices.DAL.CRM.common.ICRMUtilities>().To<JKMServices.DAL.CRM.common.CRMUtilities>();
            Bind<JKMServices.DAL.CRM.common.IDTOToCRMMapper>().To<JKMServices.DAL.CRM.common.DTOToCRMMapper>();

            // DAL > CRM 
            Bind<JKMServices.DAL.CRM.ICustomerDetails>().To<JKMServices.DAL.CRM.CustomerDetails>();
            Bind<JKMServices.DAL.CRM.IMoveDetails>().To<JKMServices.DAL.CRM.MoveDetails>();
            Bind<JKMServices.DAL.CRM.IEstimateDetails>().To<JKMServices.DAL.CRM.EstimateDetails>();
            Bind<JKMServices.DAL.CRM.IPaymentDetails>().To<JKMServices.DAL.CRM.PaymentDetails>();
            Bind<JKMServices.DAL.CRM.IAlertDetails>().To<JKMServices.DAL.CRM.AlertDetails>();

            Bind<Utility.IGenerator>().To<Utility.Generator>();
            
            Bind<Utility.Email.IEmailConfiguration>().To<Utility.Email.EmailConfiguration>();
            Bind<Utility.Email.ISmtpConfiguration>().To<Utility.Email.SmtpConfiguration>();
            Bind<Utility.Email.ISmtpClientFactory>().To<Utility.Email.SmtpClientFactory>();
            Bind<Utility.Email.IRetry>().To<Utility.Email.Retry>();

            Bind<System.Net.Mail.SmtpClient>().ToSelf();
            Bind<System.Net.Mail.MailMessage>().ToSelf();

            Bind<Utility.ISharepointConsumer>().To<Utility.SharepointConsumer>();

        }
    }
}