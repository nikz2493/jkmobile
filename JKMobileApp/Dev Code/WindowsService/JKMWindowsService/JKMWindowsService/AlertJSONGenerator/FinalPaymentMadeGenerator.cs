using JKMWindowsService.Model;
using JKMWindowsService.Utility;

namespace JKMWindowsService.AlertJSONGenerator
{
    public class FinalPaymentMadeGenerator : IFinalPaymentMadeGenerator
    {
        private readonly IResourceManagerFactory resourceManager;

        public FinalPaymentMadeGenerator(IResourceManagerFactory resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        /// Method Name     : GenerateJSON
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Feb 2018
        /// Purpose         : To Generate Final Payment Made Notification alert.
        /// Revision        : 
        /// </summary>
        public string GenerateJSON(string customerID, string alertDescription)
        {
            AlertModel alertModel = new AlertModel
            {
                AlertID = null,
                CustomerID = customerID,
                AlertTitle = resourceManager.GetString("alertTitle_FinalPaymentMade"),
                AlertDescription = resourceManager.GetString("alertDescription_FinalPaymentMade"),
                NotificationType = resourceManager.GetString("alert_notification_FinalPayment"),
                StartDate = null,
                EndDate = null,
                IsActive = true
            };

            return General.ConvertToJson(alertModel);
        }
    }
}
