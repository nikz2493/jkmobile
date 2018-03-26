using JKMWindowsService.AlertManager.Common;
using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;

namespace JKMWindowsService.AlertManager
{
    public class PreMoveConfirmationNotifications : IPreMoveConfirmationNotifications
    {
        private readonly IAPIHelper apiHelper;
        private readonly IGenericMethods genericMethods;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;
        private readonly AlertJSONGenerator.IPreMoveConfirmationNotificationGenerator preMoveConfirmationNotification;

        //Constructor
        /// <summary>
        /// Constructor Name        : AlertAPIServices
        /// Author                  : Pratik Soni
        /// Creation Date           : 14 Feb 2018
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public PreMoveConfirmationNotifications(IAPIHelper apiHelper,
                                                IGenericMethods genericMethods,
                                                AlertJSONGenerator.IPreMoveConfirmationNotificationGenerator preMoveConfirmationNotification,
                                                IResourceManagerFactory resourceManager,
                                                ILogger logger)
        {
            this.apiHelper = apiHelper;
            this.genericMethods = genericMethods;
            this.preMoveConfirmationNotification = preMoveConfirmationNotification;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : SendAlerts
        /// Author          : Pratik Soni
        /// Creation Date   : 15 Feb 2018
        /// Purpose         : To create alerts for each move
        /// Revision        : 
        /// </summary>
        public void SendAlerts()
        {
            List<MoveModel> failedMoveModelList;
            List<MoveModel> moveModelList;
            try
            {
                moveModelList = genericMethods.GetMoveModelList(resourceManager.GetString("statusReason_booked"));

                if (moveModelList.Count == 0)
                {
                    logger.Info(resourceManager.GetString("logNoMovesForBooked"));
                    return;
                }

                failedMoveModelList = new List<MoveModel>();

                AlertGenerationForAllMoves(ref failedMoveModelList, moveModelList);

                //Writes list of failed DTOs to the XML file.
                if (!General.WriteDTOListToXMLFile(failedMoveModelList, "MoveModelList.XML"))
                {
                    logger.Error("logErrorOccuredINFileWriting");
                }

                //Increment the count key
                General.UpdateConfigValueForCount();
            }
            catch (Exception ex)
            {
                logger.Error("Error occured: ", ex);
            }
        }

        /// <summary>
        /// Method Name     : AlertGenerationForAllMoves
        /// Author          : Pratik Soni
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Loops through all moves and generates alerts for all.
        /// Revision        : 
        /// <paramref name="failedMoveModelList">referenced parameter to pass the list of failed moves for which error occured while alert generation.</paramref>
        /// <paramref name="moveModelList">List of model for which alerts needs to be generated.</paramref>
        /// </summary>
        /// 
        private void AlertGenerationForAllMoves(ref List<MoveModel> failedMoveModelList, List<MoveModel> moveModelList)
        {
            string requestContentForPreMove;
            string alertDescription = string.Empty;
            string date;
            string apiPath;
            DateTime moveStartDate;
            HttpResponseMessage httpResponseMessage;

            foreach (var dtoMove in moveModelList)
            {
                //Use packDate, and if it is blank use loadDate as MoveStartDate
                date = (string.IsNullOrEmpty(dtoMove.MoveDetails_PackStartDate) ? dtoMove.MoveDetails_LoadStartDate : dtoMove.MoveDetails_PackStartDate);
                moveStartDate = DateTime.Parse(date, new CultureInfo("en-US"), DateTimeStyles.None);

                alertDescription = ValidateMoveAndGetAlertDescription(moveStartDate);
                // If current move is not satisfying the switch case, continue with next move
                if (string.IsNullOrEmpty(alertDescription))
                {
                    continue;
                }

                //Generate JSON for PreMove Notification Confirmation
                requestContentForPreMove = preMoveConfirmationNotification.GenerateJSON(dtoMove.ContactOfMoveId,
                                                                                        moveStartDate.ToString(resourceManager.GetString("ShortUSDateFormat")),
                                                                                        moveStartDate.AddDays(1).ToString(resourceManager.GetString("ShortUSDateFormat")),
                                                                                        alertDescription);

                //POST AlertJson in CRM
                apiPath = General.GetAPIPath(resourceManager.GetString("AlertService"), dtoMove.ContactOfMoveId);
                httpResponseMessage = apiHelper.InvokePostAPI(apiPath, requestContentForPreMove);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    logger.Info(resourceManager.GetString("logAlertCreatedForMove") + dtoMove.MoveId);
                    failedMoveModelList.Add(dtoMove);
                }
            }
        }

        /// <summary>
        /// Method Name     : ValidateMoveAndGetAlertDescription
        /// Author          : Pratik Soni
        /// Creation Date   : 23 Feb 2018
        /// Purpose         : To validate the move by date to check if it sets to any pre-move condition
        /// Revision        : 
        /// </summary>
        /// <returns>blank if date difference is not equal to 5/4/3 </returns>
        private string ValidateMoveAndGetAlertDescription(DateTime moveStartDate)
        {
            const int PreMove_5Days = 5;
            const int PreMove_4Days = 4;
            const int PreMove_3Days = 3;

            string alertDescription = string.Empty;
            TimeSpan dateDifference = moveStartDate - DateTime.Now;
            switch (dateDifference.Days)
            {
                case PreMove_5Days:
                    alertDescription = resourceManager.GetString("alertDescription_FirstPreMoveConfirmationNotification");
                    break;
                case PreMove_4Days:
                    alertDescription = resourceManager.GetString("alertDescription_SecondPreMoveConfirmationNotification");
                    break;
                case PreMove_3Days:
                    alertDescription = resourceManager.GetString("alertDescription_ThirdPreMoveConfirmationNotification");
                    break;
            }
            return alertDescription;
        }
    }
}
