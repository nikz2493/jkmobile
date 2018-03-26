using JKMWindowsService.AlertManager.Common;
using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace JKMWindowsService.AlertManager
{
    public class FinalPaymentMade : IFinalPaymentMade
    {
        private readonly IAPIHelper apiHelper;
        private readonly IGenericMethods genericMethods;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;
        private readonly AlertJSONGenerator.IFinalPaymentMadeGenerator finalPaymentMadeGenerator;

        /// <summary>
        /// Constructor Name        : FinalPaymentMade
        /// Author                  : Ranjana Singh
        /// Creation Date           : 28 Feb 2018
        /// Purpose                 : To create instance of FinalPaymentMade class
        /// Revision                : 
        /// </summary>
        public FinalPaymentMade(IAPIHelper apiHelper,
                            IGenericMethods genericMethods,
                            AlertJSONGenerator.IFinalPaymentMadeGenerator finalPaymentMadeGenerator,
                            IResourceManagerFactory resourceManager,
                            ILogger logger)
        {
            this.apiHelper = apiHelper;
            this.genericMethods = genericMethods;
            this.finalPaymentMadeGenerator = finalPaymentMadeGenerator;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : SendAlerts
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Feb 2018
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
                if (!General.WriteDTOListToXMLFile<List<MoveModel>>(failedMoveModelList, "MoveModelList.XML"))
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
        /// Author          : Ranjana Singh
        /// Creation Date   : 28 Feb 2018
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
                requestContentForBookedMove = finalPaymentMadeGenerator.GenerateJSON(dtoMove.ContactOfMoveId, resourceManager.GetString("alertDescription_FinalPaymentMade"));

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
