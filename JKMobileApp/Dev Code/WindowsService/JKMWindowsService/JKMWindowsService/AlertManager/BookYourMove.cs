using JKMWindowsService.AlertManager.Common;
using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace JKMWindowsService.AlertManager
{
    public class BookYourMove : IBookYourMove
    {
        private readonly IAPIHelper apiHelper;
        private readonly IGenericMethods genericMethods;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;
        private readonly AlertJSONGenerator.IBookYourMoveGenerator bookYourMoveGenerator;

        //Constructor
        /// <summary>
        /// Constructor Name        : AlertAPIServices
        /// Author                  : Pratik Soni
        /// Creation Date           : 14 Feb 2018
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public BookYourMove(IAPIHelper apiHelper,
                            IGenericMethods genericMethods,
                            AlertJSONGenerator.IBookYourMoveGenerator bookYourMoveGenerator,
                            IResourceManagerFactory resourceManager,
                            ILogger logger)
        {
            this.apiHelper = apiHelper;
            this.genericMethods = genericMethods;
            this.bookYourMoveGenerator = bookYourMoveGenerator;
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
                moveModelList = genericMethods.GetMoveModelList(resourceManager.GetString("statusReason_ordered"));

                if (moveModelList.Count == 0)
                {
                    logger.Info(resourceManager.GetString("logNoMovesForOrdered"));
                    return;
                }

                failedMoveModelList = new List<MoveModel>();

                AlertGenerationForAllOrderedMoves(ref failedMoveModelList, moveModelList);

                //Writes list of failed DTOs to the XML file.
                if (!General.WriteDTOListToXMLFile<List<MoveModel>>(failedMoveModelList, resourceManager.GetString("fileName_OrderedMoveModelList")))
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
        /// Method Name     : AlertGenerationForAllOrderedMoves
        /// Author          : Pratik Soni
        /// Creation Date   : 26 Feb 2018
        /// Purpose         : Loops through all moves and generates alerts for all.
        /// Revision        : 
        /// <paramref name="failedMoveModelList">referenced parameter to pass the list of failed moves for which error occured while alert generation.</paramref>
        /// <paramref name="moveModelList">List of model for which alerts needs to be generated.</paramref>
        /// </summary>
        /// 
        private void AlertGenerationForAllOrderedMoves(ref List<MoveModel> failedMoveModelList, List<MoveModel> moveModelList)
        {
            string requestContentForBookedMove;
            string apiPath;
            HttpResponseMessage httpResponseMessage;

            foreach (var dtoMove in moveModelList)
            {
                //Generate JSON for Booked move (which are in ORDERED status) Notification Confirmation
                requestContentForBookedMove = bookYourMoveGenerator.GenerateJSON(dtoMove.ContactOfMoveId, resourceManager.GetString("alertDescription_BookYourMove"));

                //POST AlertJson in CRM
                apiPath = General.GetAPIPath(resourceManager.GetString("AlertService"), dtoMove.ContactOfMoveId);
                httpResponseMessage = apiHelper.InvokePostAPI(apiPath, requestContentForBookedMove);

                if (!httpResponseMessage.IsSuccessStatusCode)
                {
                    logger.Info(resourceManager.GetString("logAlertCreatedForMove") + dtoMove.MoveId);
                    failedMoveModelList.Add(dtoMove);
                }
            }
        }
    }
}
