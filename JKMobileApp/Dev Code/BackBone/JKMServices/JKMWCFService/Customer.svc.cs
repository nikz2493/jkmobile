using Newtonsoft.Json.Linq;
using Ninject;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Customer
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Gets all the information of Customer. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Customer : ICustomer
    {
        private readonly JKMServices.BLL.Interface.ICustomerDetails bllCustomerDetails;
        private readonly StandardKernel kernel;

        public Customer()
        {
            log4net.Config.XmlConfigurator.Configure();
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = Utility.General.StandardKernel();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllCustomerDetails = Utility.General.Resolve<JKMServices.BLL.Interface.ICustomerDetails>();
        }

        /// <summary>
        /// Method Name     : GetCustomerID
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the unique CustomerID to identify the rest of the service requests.
        /// Revision        : By Vivek Bhavsar on 22-12-2017 : Call bll using NInject standard kernal
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns>JObject</returns>
        public string GetCustomerID(string email)
        {
            string serviceResponse = bllCustomerDetails.GetCustomerID(email).ToString().Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetCustomerProfileData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets all the profile data for the customer. 
        /// Revision        :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string GetCustomerProfileData(string customerID)
        {
            string serviceResponse = bllCustomerDetails.GetCustomerProfileData(customerID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary> 
        /// Method Name     : GetCustomerVerificationData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets all the login data for the customer.
        /// Revision        :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string GetCustomerVerificationData(string customerID)
        {
            string serviceResponse = bllCustomerDetails.GetCustomerVerificationData(customerID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PostCustomerProfileData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Posts all the profile data of the customer.
        /// Revision        :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PostCustomerProfileData(string customerID)
        {
            return customerID;
        }

        /// <summary>
        /// Method Name     : PostCustomerVerificationData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Posts all the login data for the customer.
        /// Revision        :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PostCustomerVerificationData(string customerID)
        {
            return customerID;
        }

        /// <summary>
        /// Method Name     : PutCustomerProfileData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Updates all the profile data of the customer.
        /// Revision        : By Pratik Soni on 12 Dec 2017: Implemented code
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PutCustomerProfileData(string customerID, CustomerProfile customerProfile)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(customerProfile);
            string serviceResponse = bllCustomerDetails.PutCustomerProfileData(customerID, requestBody);
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PutCustomerVerificationData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Updates all the login data for the customer.
        /// Revision        :
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PutCustomerVerificationData(string customerID, CustomerVerification customerVerification)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(customerVerification);
            string serviceResponse = bllCustomerDetails.PutCustomerVerificationData(customerID, requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }
    }
}
