using JKMWindowsService.Model;
using JKMWindowsService.Utility;
using JKMWindowsService.Utility.Log;
using System.Collections.Generic;
using System.Configuration;

namespace JKMWindowsService.AlertManager.Common
{
    public class GenericMethods : IGenericMethods
    {
        private readonly MoveManager.IMoveDetails moveDetails;
        private readonly IResourceManagerFactory resourceManager;
        private readonly ILogger logger;

        //Constructor
        /// <summary>
        /// Constructor Name        : AlertAPIServices
        /// Author                  : Pratik Soni
        /// Creation Date           : 27 Feb 2018
        /// Purpose                 : To create instant of APIHelper class
        /// Revision                : 
        /// </summary>
        public GenericMethods(MoveManager.IMoveDetails moveDetails,
                              IResourceManagerFactory resourceManager,
                              ILogger logger)
        {
            this.moveDetails = moveDetails;
            this.resourceManager = resourceManager;
            this.logger = logger;
        }

        /// <summary>
        /// Method Name     : GetMoveModelList
        /// Author          : Pratik Soni
        /// Creation Date   : 27 Feb 2018
        /// Purpose         : Gets the list of Move Model - either from file or from CRM
        /// Revision        : 
        /// </summary>
        /// <returns> List of MoveModel </returns>
        /// 
        public List<MoveModel> GetMoveModelList(string statusReason)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            List<MoveModel> moveModelList = new List<MoveModel>();
            if (int.Parse(config.AppSettings.Settings["Count"].Value) <= int.Parse(config.AppSettings.Settings["MaximumRowCount"].Value))
            {
                moveModelList = General.ReadDTOListFromXML<List<MoveModel>>(resourceManager.GetString("fileName_MoveModelList"));
                //To reset the 'Count' key of appConfig
                General.UpdateConfigValueForCount(true);
            }

            if (moveModelList == null || int.Parse(config.AppSettings.Settings["Count"].Value) > int.Parse(config.AppSettings.Settings["MaximumRowCount"].Value))
            {
                moveModelList = moveDetails.GetMoveList(statusReason);
                if (!General.WriteDTOListToXMLFile<List<MoveModel>>(moveModelList, resourceManager.GetString("fileName_MoveModelList")))
                {
                    logger.Error(resourceManager.GetString("logErrorOccuredINFileWriting"));
                    return new List<MoveModel>();
                }
            }
            return moveModelList;
        }
    }
}