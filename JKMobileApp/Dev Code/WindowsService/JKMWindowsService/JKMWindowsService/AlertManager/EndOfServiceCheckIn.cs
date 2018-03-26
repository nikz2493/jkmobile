using JKMWindowsService.AlertManager.Common;
using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace JKMWindowsService.AlertManager
{
    public class EndOfServiceCheckIn : IEndOfServiceCheckIn
    {
        private readonly IAPIHelper apiHelper;
        private readonly IGenericMethods genericMethods;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;
        private readonly AlertJSONGenerator.IEndOfServiceCheckInGenerator endOfServiceCheckInGenerator;

        //Constructor
        /// <summary>
        /// Constructor Name        : BeginningOfDayOfServiceCheckIn
        /// Author                  : Pratik Soni
        /// Creation Date           : 28 Feb 2018
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public EndOfServiceCheckIn(IAPIHelper apiHelper,
                                              IGenericMethods genericMethods,
                                              AlertJSONGenerator.IEndOfServiceCheckInGenerator endOfServiceCheckInGenerator,
                                              IResourceManagerFactory resourceManager,
                                              ILogger logger)
        {
            this.apiHelper = apiHelper;
            this.genericMethods = genericMethods;
            this.endOfServiceCheckInGenerator = endOfServiceCheckInGenerator;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : SendAlerts
        /// Author          : Pratik Soni
        /// Creation Date   : 28 Feb 2018
        /// Purpose         : To create alerts for each move
        /// Revision        : 
        /// </summary>
        public void SendAlerts()
        {
            List<MoveModel> moveModelList;
            DateTime startTime;
            double timeDifferenceInMinutes;

            try
            {
                startTime = DateTime.Parse(DateTime.Now.ToShortTimeString());
                timeDifferenceInMinutes = DateTime.Parse(ConfigurationManager.AppSettings["StartTimeForEndOfService"]).Subtract(startTime).TotalMinutes;
                if (timeDifferenceInMinutes == double.Parse(ConfigurationManager.AppSettings["DifferenceForTimeStartInMinutes"]))
                {
                    //Get move list for booked, packed, loaded
                    moveModelList = GetMoveList();
                    if (moveModelList.Count == 0)
                    {
                        logger.Info(resourceManager.GetString("logNoMovesFound"));
                    }
                    else
                    {
                        AlertGenerationForAllMoves(moveModelList);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error occured: ", ex);
            }
        }

        /// <summary>
        /// Method Name     : GetMoveList
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Mar 2018
        /// Purpose         : Gets list of move list for packed, loaded and booked
        /// Revision        : 
        /// </summary>
        /// 
        private List<MoveModel> GetMoveList()
        {
            List<MoveModel> moveModelList;
            moveModelList = genericMethods.GetMoveModelList(resourceManager.GetString("statusReason_booked"));
            moveModelList.AddRange(genericMethods.GetMoveModelList(resourceManager.GetString("statusReason_packed")));
            moveModelList.AddRange(genericMethods.GetMoveModelList(resourceManager.GetString("statusReason_loaded")));
            return moveModelList;
        }

        /// <summary>
        /// Method Name     : AlertGenerationForAllOrderedMoves
        /// Author          : Pratik Soni
        /// Creation Date   : 28 Feb 2018
        /// Purpose         : Loops through all moves and generates alerts for all.
        /// Revision        : 
        /// <param name="moveModelList">List of model for which alerts needs to be generated.</param>
        /// </summary>
        /// 
        private void AlertGenerationForAllMoves(List<MoveModel> moveModelList)
        {
            string requestContentForBookedMove;
            string apiPath;
            HttpResponseMessage httpResponseMessage;
            string startDate;

            foreach (var dtoMove in moveModelList)
            {
                startDate = GetBeginningDayOfService(dtoMove);
                if (string.IsNullOrEmpty(startDate))
                {
                    continue;
                }

                //Generate JSON for Booked move (which are in ORDERED status) Notification Confirmation
                requestContentForBookedMove = endOfServiceCheckInGenerator.GenerateJSON(dtoMove.ContactOfMoveId, startDate);

                //POST AlertJson in CRM
                apiPath = General.GetAPIPath(resourceManager.GetString("AlertService"), dtoMove.ContactOfMoveId);
                httpResponseMessage = apiHelper.InvokePostAPI(apiPath, requestContentForBookedMove);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    logger.Info(resourceManager.GetString("logAlertCreatedForMove") + dtoMove.MoveId);
                }
            }
        }

        /// <summary>
        /// Method Name     : GetBeginningDayOfService
        /// Author          : Pratik Soni
        /// Creation Date   : 01 Mar 2018
        /// Purpose         : To get any day of service if it match to the current date
        /// Revision        : 
        /// <param name="dtoMove"/>
        /// </summary>
        /// <returns> date in STRING type, if no date matches then return empty</returns>
        private string GetBeginningDayOfService(MoveModel dtoMove)
        {
            DateTime currentDate = DateTime.Parse(DateTime.Now.ToString("MM-dd-yyyy"));

            if (DateTime.Parse(dtoMove.MoveDetails_PackStartDate) == currentDate)
            {
                return dtoMove.MoveDetails_PackStartDate;
            }
            else if (DateTime.Parse(dtoMove.MoveDetails_LoadStartDate) == currentDate)
            {
                return dtoMove.MoveDetails_LoadStartDate;
            }
            else if (DateTime.Parse(dtoMove.MoveDetails_DeliveryStartDate) == currentDate)
            {
                return dtoMove.MoveDetails_DeliveryStartDate;
            }
            return string.Empty;
        }
    }
}