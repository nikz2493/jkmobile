using JKMWindowsService.Model;
using JKMWindowsService.Utility;

namespace JKMWindowsService.AlertJSONGenerator
{
    public class EndOfServiceCheckInGenerator : IEndOfServiceCheckInGenerator
    {
        private readonly IResourceManagerFactory resourceManager;

        public EndOfServiceCheckInGenerator(IResourceManagerFactory resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        /// Method Name     : GenerateJSON
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Mar 2018
        /// Purpose         : To Generate End of Service Check-In notification alert
        /// Revision        : 
        /// </summary>
        public string GenerateJSON(string customerID, string startDate)
        {
            AlertModel alertModel = new AlertModel
            {
                AlertID = null,
                CustomerID = customerID,
                AlertTitle = resourceManager.GetString("alertTitle_EndOfServiceCheckIn"),
                AlertDescription = resourceManager.GetString("alertDescription_EndOfServiceCheckIn"),
                NotificationType = resourceManager.GetString("alert_notification_BeginningOfDayOfServiceCheckIn"),
                StartDate = startDate,
                EndDate = startDate,    //Alert won't appear on the customer's notification screen once response is provided or day of service is over
                IsActive = true
            };

            return General.ConvertToJson(alertModel);
        }
    }
}