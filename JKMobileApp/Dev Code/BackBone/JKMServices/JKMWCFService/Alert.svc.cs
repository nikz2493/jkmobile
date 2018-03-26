using Newtonsoft.Json.Linq;
using Ninject;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Alert
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Gets the list of all alerts. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Alert : IAlert
    {
        //BLL.ContactDetails class object to
        private readonly JKMServices.BLL.Interface.IAlertDetails bllAlertDetails;
        private readonly StandardKernel kernel;

        //Constructor
        public Alert()
        {
            log4net.Config.XmlConfigurator.Configure();
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllAlertDetails = kernel.Get<JKMServices.BLL.Interface.IAlertDetails>();
        }

        /// <summary>
        /// Method Name     : GetAlertList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the list of all alerts since the given date.
        /// Revision        :
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public string GetAlertList(string customerID, string startDate = null)
        {
            string serviceResponse = bllAlertDetails.GetAlertList(customerID, startDate).ToString().Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name      : PostAlertList
        /// Author           : Ranjana Singh
        /// Creation Date    : 27 Nov 2017
        /// Purpose          : Creates new alert records. Requires all mandatory fields in the request body
        /// Revision         :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PostAlertList(string customerID, AlertDetail alertDetails)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(alertDetails);
            string serviceResponse = bllAlertDetails.PostAlertList(customerID, requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PutAlertList
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Updates information for the all the alerts in the list. 
        /// Revision        :
        /// </summary>
        /// <param name="alertId"></param>        
        /// <returns></returns>
        public string PutAlertList(string alertID, AlertDetail alertDetails)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(alertDetails);
            string serviceResponse = bllAlertDetails.PutAlertList(alertID,requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }      
    }
}