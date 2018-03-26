using Newtonsoft.Json.Linq;
using Ninject;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Estimate
    /// Author          : Pratik Soni
    /// Creation Date   : 10 Jan 2018
    /// Purpose         : Gets the list of all Estimate. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Estimate : IEstimate
    {
        //BLL.ContactDetails class object to
        private readonly JKMServices.BLL.Interface.IEstimateDetails bllEstimateDetails;
        private readonly StandardKernel kernel;

        //Constructor
        public Estimate()
        {
			log4net.Config.XmlConfigurator.Configure();
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = Utility.General.StandardKernel();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllEstimateDetails = kernel.Get<JKMServices.BLL.Interface.IEstimateDetails>();
        }
        /// <summary>
        /// Method Name     : GetEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : Gets the estimate data for the customer id.
        /// Revision        :
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetEstimateData(string customerId)
        {
            string serviceResponse;
            serviceResponse = bllEstimateDetails.GetEstimateData(customerId).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetEstimatePDF
        /// Author          : Pratik Soni
        /// Creation Date   : 10 Jan 2018
        /// Purpose         : Gets the estimate PDF file for the estimateID in the request body.
        /// Revision        :
        /// </summary>
        /// <param name="estimateId"></param>
        /// <returns></returns>
        public string GetEstimatePDF(string moveId)
        {
            string serviceResponse;
            serviceResponse = bllEstimateDetails.GetEstimatePDF(moveId).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PutEstimateData
        /// Author          : Pratik Soni
        /// Creation Date   : 12 Jan 2018
        /// Purpose         : Updates estimate data for the moveId. Mostly used to set estimate as booked.
        /// Revision        :
        /// </summary>
        /// <param name="moveId"></param>
        /// <returns></returns>
        public string PutEstimateData(string moveId, EstimateDetail estimateDetail)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(estimateDetail);
            string serviceResponse = bllEstimateDetails.PutEstimateData(moveId, requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }
    }
}
