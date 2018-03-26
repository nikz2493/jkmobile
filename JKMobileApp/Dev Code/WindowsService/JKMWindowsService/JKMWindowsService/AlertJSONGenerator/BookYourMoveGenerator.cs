using JKMWindowsService.Model;
using JKMWindowsService.Utility;

namespace JKMWindowsService.AlertJSONGenerator
{
    public class BookYourMoveGenerator : IBookYourMoveGenerator
    {
        private readonly IResourceManagerFactory resourceManager;

        public BookYourMoveGenerator(IResourceManagerFactory resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        /// Method Name     : GenerateJSON
        /// Author          : Pratik Soni
        /// Creation Date   : 27 Feb 2018
        /// Purpose         : To Generate Pre-Move Confirmation Notification alert - 5 DAYS BEFORE
        /// Revision        : 
        /// </summary>
        public string GenerateJSON(string customerID, string alertDescription)
        {
            AlertModel alertModel = new AlertModel
            {
                AlertID = null,
                CustomerID = customerID,
                AlertTitle = resourceManager.GetString("alertTitle_BookYourMove"),
                AlertDescription = resourceManager.GetString("alertDescription_BookYourMove"),
                NotificationType = resourceManager.GetString("alert_notification_BookMove"),
                StartDate = null,
                EndDate = null,
                IsActive = true
            };

            return General.ConvertToJson(alertModel);
        }
    }
}
