using JKMWindowsService.Model;
using JKMWindowsService.Utility;

namespace JKMWindowsService.AlertJSONGenerator
{
    public class PreMoveConfirmationNotificationGenerator : IPreMoveConfirmationNotificationGenerator
    {
        private readonly IResourceManagerFactory resourceManager;
        private readonly Utility.Log.ILogger logger;

        public PreMoveConfirmationNotificationGenerator(IResourceManagerFactory resourceManager,
                                                        Utility.Log.ILogger logger)
        {
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GenerateJSON
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : To Generate Pre-Move Confirmation Notification alert - 5 DAYS BEFORE
        /// Revision        : 
        /// </summary>
        public string GenerateJSON(string customerID, string alertStartDate, string alertEndDate, string alertDescription)
        {
            AlertModel alertModel = new AlertModel
            {
                AlertID = null,
                CustomerID = customerID,
                AlertTitle = resourceManager.GetString("alertTitle_PreMoveConfirmation"),
                AlertDescription = alertDescription,
                NotificationType = resourceManager.GetString("alert_notification_Pre-MoveConfirmation"),
                StartDate = alertStartDate,
                EndDate = alertEndDate,
                IsActive = true
            };

            logger.Info(resourceManager.GetString("logJSONCreated"));
            return General.ConvertToJson<AlertModel>(alertModel);
        }
    }
}
