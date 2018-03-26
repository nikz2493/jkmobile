using Newtonsoft.Json.Linq;
using Ninject;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.Web.Script.Serialization;

namespace JKMWCFService
{
    /// <summary>
    /// Class Name      : Move
    /// Author          : Ranjana Singh
    /// Creation Date   : 27 Nov 2017
    /// Purpose         : Gets all the information of Move. 
    /// Revision        :
    /// </summary>
    /// <returns></returns>
    public class Move : IMove
    {
        //BLL.ContactDetails class object to
        private readonly JKMServices.BLL.Interface.IMoveDetails bllMoveDetails;
        private readonly StandardKernel kernel;

        //Constructor
        public Move()
        {
            StandardKernelLoader standardKernelLoader = new StandardKernelLoader();
            kernel = Utility.General.StandardKernel();
            kernel = standardKernelLoader.LoadStandardKernel();
            bllMoveDetails = kernel.Get<JKMServices.BLL.Interface.IMoveDetails>();
        }

        /// <summary>
        /// Method Name     : GetContactListForMove
        /// Author          : Pratik Soni
        /// Creation Date   : 19 Dec 2017
        /// Purpose         : Gets the list of contacts for the specified move ID.  
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetContactForMove(string moveID)
        {
            string serviceResponse;
            serviceResponse = bllMoveDetails.GetContactForMove(moveID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetMoveData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the full move data, including addresses, dates, services selected, current status.  
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetMoveData(string moveID)
        {
            string serviceResponse;
            serviceResponse = bllMoveDetails.GetMoveData(moveID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetMoveID
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Gets the active move for the Customer.
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetMoveID(string customerID)
        {
            string serviceResponse;
            serviceResponse = bllMoveDetails.GetMoveID(customerID).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : GetMoveList
        /// Author          : Pratik Soni
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : Gets the active move list from JIM
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string GetMoveList(string statusReason)
        {
            string serviceResponse;
            serviceResponse = bllMoveDetails.GetMoveList(statusReason).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }

        /// <summary>
        /// Method Name     : PutMoveData
        /// Author          : Ranjana Singh
        /// Creation Date   : 27 Nov 2017
        /// Purpose         : Updates move data like current status for the specified move ID. 
        /// Revision        :
        /// </summary>
        /// <returns></returns>
        public string PutMoveData(string moveID, MoveDetail moveDetail)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string requestBody = js.Serialize(moveDetail);
            string serviceResponse = bllMoveDetails.PutMoveData(moveID, requestBody).Replace("\"", "'");
            WebOperationContext customContext;
            customContext = Utility.General.SetCustomHttpStatusCode(JObject.Parse(serviceResponse));
            return serviceResponse;
        }
    }
}
