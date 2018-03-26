using Newtonsoft.Json.Linq;
using Ninject;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Payment
    /// Author          : Ranjana Singh
    /// Creation Date   : 23 Jan 2018
    /// Purpose         : Gets all the information of Payment. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Payment : IPayment
    {
        private readonly JKMServices.BLL.Interface.IPaymentDetails bllPaymentDetails;
        private readonly StandardKernel kernel;

        public Payment()
        {
            log4net.Config.XmlConfigurator.Configure();
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = Utility.General.StandardKernel();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllPaymentDetails = Utility.General.Resolve<JKMServices.BLL.Interface.IPaymentDetails>();
        }

        /// <summary>
        /// Method Name     : GetTransactionHistory
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : customerID for the customer whose transaction history is to be retrieved
        /// Revision        : N/A
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string GetTransactionHistory(string customerID)
        {
            return customerID;
        }

        /// <summary>
        /// Method Name     : GetAmount
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Feb 2018
        /// Purpose         : To retrive customer's payment details
        /// Revision        : N/A
        /// </summary>
        /// <param name="customerID">for the customer whose amount payable information is to be retrieved.</param>
        /// <returns></returns>
        public string GetAmount(string moveID)
        {
            string serviceResponse = bllPaymentDetails.GetAmount(moveID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetDeviceID
        /// Author          : Pratik Soni
        /// Creation Date   : 07 Feb 2018
        /// Purpose         : customerID for the customer whose amount payable information is to be retrieved.
        /// Revision        : N/A
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string GetDeviceID()
        {
            string serviceResponse = bllPaymentDetails.GetDeviceID().Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PutAmount
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Feb 2018
        /// Purpose         : customerID for the customer whose amount payable information is to be updated.
        /// Revision        : N/A
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PutAmount(PaymentDetails requestContent)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(requestContent);
            string serviceResponse = bllPaymentDetails.PutAmount(requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PostTransactionHistory
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : Creates new records for transaction history.
        /// Revision        : By Pratik Soni on 31 Jan 2018: Removing LIST object for PostTransactionHistory
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public string PostTransactionHistory(TransactionDetail transactionDetails)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(transactionDetails);
            string serviceResponse = bllPaymentDetails.PostTransactionHistory(requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : DeleteTransactionHistory
        /// Author          : Ranjana Singh
        /// Creation Date   : 23 Jan 2018
        /// Purpose         : ID of the transaction to be deleted. This ID can be retrieved using the GET method.
        /// Revision        : N/A
        /// </summary>
        /// <returns></returns>
        public string DeleteTransactionHistory()
        {
            return null;
        }
    }
}
